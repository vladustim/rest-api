const content = document.querySelector("#content");
const deliveryNames = {
  Department: "Відділення",
  Courier: "Кур’єр",
  ExpressCourier: "Експрес-кур’єр"
};
const money = new Intl.NumberFormat("uk-UA", { minimumFractionDigits: 2, maximumFractionDigits: 2 });
const kilograms = new Intl.NumberFormat("uk-UA", { maximumFractionDigits: 4 });
const numericFields = ["weight", "depth", "width", "height"];
let routeController;
let selectedId;
let notice;

class ApiError extends Error {
  constructor(status, problem) {
    super(problem?.detail ?? problem?.title ?? "");
    this.status = status;
    this.fields = problem?.errors;
  }
}

async function request(path, { method = "GET", body, signal } = {}) {
  const deadline = AbortSignal.timeout(10000);
  const response = await fetch(path, {
    method, signal: signal ? AbortSignal.any([signal, deadline]) : deadline, cache: "no-store",
    ...(body === undefined ? {} : {
      headers: { "Content-Type": "application/json" }, body: JSON.stringify(body)
    })
  });
  if (!response.ok) {
    const problem = await response.json().catch(() => null);
    throw new ApiError(response.status, problem);
  }
  return response.status === 204 ? null : response.json();
}

function errorText(error) {
  if (error.name === "TimeoutError") return "Сервер не відповів вчасно.";
  if (error.status === 404) return "Відправлення більше не існує.";
  if (error.status === 400 || error.status === 422) return "Сервер відхилив дані. Перевірте заповнені поля.";
  if (error.status) return "Сервер повернув помилку HTTP " + error.status + ".";
  return "Не вдалося отримати відповідь від PostService. Перевірте з’єднання.";
}

function show(element, text = "") {
  element.textContent = text;
  element.hidden = !text;
}

function element(tag, text, className) {
  const node = document.createElement(tag);
  if (text !== undefined) node.textContent = text;
  if (className) node.className = className;
  return node;
}

function openList(message) {
  notice = message;
  location.hash = "/shipments";
}

function renderRoute() {
  routeController?.abort();
  routeController = new AbortController();
  const { signal } = routeController;
  const path = location.hash.slice(1) || "/";
  const item = /^\/shipments\/([1-9]\d*)(\/edit)?$/.exec(path);
  const id = item && Number(item[1]) <= 2147483647 ? Number(item[1]) : null;
  let page = { "/": "home", "/about": "about", "/shipments": "shipments", "/shipments/new": "editor" }[path];
  if (id !== null) page = item[2] ? "editor" : "details";
  if (typeof page !== "string") {
    content.replaceChildren(element("h1", "Сторінку не знайдено"));
    const link = element("a", "← На головну", "button");
    link.href = "#/";
    content.append(link);
  } else {
    content.replaceChildren(document.getElementById(page).content.cloneNode(true));
    if (page === "shipments") void renderList(signal);
    if (page === "editor") void renderEditor(id, signal);
    if (page === "details") void renderDetails(id, signal);
  }

  const section = ["editor", "details"].includes(page) ? "shipments" : page;
  document.querySelectorAll("nav a").forEach(link => {
    if (link.dataset.section === section) {
      link.setAttribute("aria-current", page === section ? "page" : "location");
    } else {
      link.removeAttribute("aria-current");
    }
  });
  document.title = page === "home" ? "PostService" : content.querySelector("h1").textContent + " · PostService";
  content.focus({ preventScroll: true });
  window.scrollTo(0, 0);
}

function priceLabel(price) {
  return money.format(price) + " грн";
}

function weightLabel(weight) {
  return kilograms.format(weight) + " кг";
}

function deliveryLabel(type) {
  return deliveryNames[type] ?? "Невідомий тип доставки";
}

function selectionLabel(posting, group, select) {
  const label = element("label", undefined, "number");
  const radio = element("input");
  radio.type = "radio";
  radio.name = group;
  radio.checked = posting.id === selectedId;
  radio.setAttribute("aria-label", "Вибрати відправлення № " + posting.id);
  radio.addEventListener("change", () => select(posting.id));
  label.append(radio, document.createTextNode("№ " + posting.id));
  return label;
}

function appendDetails(target, values) {
  for (const [label, value] of values) {
    const pair = document.createDocumentFragment();
    pair.append(element("dt", label), element("dd", String(value)));
    if (target.classList.contains("details")) {
      const group = element("div");
      group.append(pair);
      target.append(group);
    } else {
      target.append(pair);
    }
  }
}

async function renderList(signal) {
  const table = content.querySelector(".table-panel");
  const tbody = table.querySelector("tbody");
  const cards = content.querySelector(".cards");
  const status = content.querySelector("[data-list-status]");
  const error = content.querySelector("[data-error]");
  const message = content.querySelector("[data-notice]");
  const toolbar = content.querySelector(".desktop-actions");
  const dialog = content.querySelector("dialog");
  const deleteForm = dialog.querySelector("form");
  const cancel = dialog.querySelector("[data-cancel]");
  const confirm = deleteForm.querySelector("button[type=submit]");
  const deleteError = dialog.querySelector("[data-delete-error]");
  let postings = [];
  let deleteId;
  let deleting = false;

  show(message, notice);
  notice = undefined;

  function perform(action, posting) {
    if (!posting) return;
    if (action !== "delete") {
      location.hash = "/shipments/" + posting.id + (action === "edit" ? "/edit" : "");
      return;
    }

    deleteId = posting.id;
    dialog.querySelector("[data-delete-description]").textContent =
      "Відправлення № " + posting.id + ". Отримувач: " + posting.to;
    show(deleteError);
    dialog.showModal();
  }

  toolbar.addEventListener("click", event => {
    const button = event.target.closest("[data-action]");
    if (button) perform(button.dataset.action, postings.find(posting => posting.id === selectedId));
  });
  cancel.addEventListener("click", () => dialog.close());
  dialog.addEventListener("cancel", event => {
    if (deleting) event.preventDefault();
  });
  deleteForm.addEventListener("submit", async event => {
    event.preventDefault();
    if (deleting || deleteId === undefined) return;
    deleting = cancel.disabled = confirm.disabled = true;
    show(deleteError);
    try {
      await request("/postings/" + deleteId, { method: "DELETE" });
      if (signal.aborted) return;
      dialog.close();
      show(message, "Відправлення видалено.");
      await load();
    } catch (failure) {
      if (signal.aborted) return;
      if (failure.status === 404) {
        dialog.close();
        show(message, errorText(failure));
        await load();
      } else {
        show(deleteError, "Не вдалося підтвердити видалення. " + errorText(failure));
      }
    } finally {
      deleting = cancel.disabled = confirm.disabled = false;
    }
  });

  function select(id) {
    selectedId = id;
    for (const container of [tbody, cards]) {
      for (const row of container.children) {
        const selected = Number(row.dataset.id) === id;
        row.classList.toggle("selected", selected);
        row.querySelector("input").checked = selected;
        const actions = row.querySelector(".actions");
        if (actions) actions.hidden = !selected;
      }
    }
    toolbar.querySelectorAll("button").forEach(button => { button.disabled = !id; });
  }

  function fill() {
    if (!postings.some(posting => posting.id === selectedId)) selectedId = postings[0]?.id;
    tbody.replaceChildren();
    cards.replaceChildren();
    for (const posting of postings) {
      const row = element("tr");
      row.dataset.id = posting.id;
      const idCell = element("td");
      idCell.append(selectionLabel(posting, "table-selection", select));
      row.append(idCell);
      for (const value of [posting.from, posting.to, deliveryLabel(posting.deliveryType),
        weightLabel(posting.weight), priceLabel(posting.price)]) row.append(element("td", value));
      row.addEventListener("click", () => select(posting.id));
      tbody.append(row);

      const card = element("li", undefined, "panel card");
      card.dataset.id = posting.id;
      const header = element("div", undefined, "card-header");
      header.append(selectionLabel(posting, "card-selection", select), element("strong", priceLabel(posting.price)));
      const details = element("dl");
      appendDetails(details, [["Відправник", posting.from], ["Отримувач", posting.to],
        ["Доставка", deliveryLabel(posting.deliveryType)], ["Вага", weightLabel(posting.weight)]]);
      const actions = element("div", undefined, "actions");
      for (const [action, label] of [["details", "Деталі"], ["edit", "Редагувати"], ["delete", "Видалити"]]) {
        const button = element("button", label, action === "delete" ? "danger-outline" : undefined);
        button.type = "button";
        button.addEventListener("click", () => perform(action, posting));
        actions.append(button);
      }
      card.append(header, details, actions);
      cards.append(card);
    }
    select(selectedId);
    table.hidden = postings.length === 0;
    show(status, postings.length ? "Усього відправлень: " + postings.length : "Список відправлень порожній.");
  }

  async function load() {
    show(status, "Завантаження відправлень…");
    show(error);
    try {
      postings = await request("/postings", { signal });
      if (!signal.aborted) fill();
    } catch (failure) {
      if (signal.aborted) return;
      postings = [];
      fill();
      show(status);
      show(error, "Не вдалося завантажити список. " + errorText(failure));
      const retry = element("button", "Повторити");
      retry.type = "button";
      retry.addEventListener("click", () => void load());
      error.append(" ", retry);
    }
  }

  await load();
}

function parseNumber(value) {
  const text = value.trim().replace(",", ".");
  return /^[+-]?(?:\d+(?:\.\d*)?|\.\d+)$/.test(text) ? Number(text) : NaN;
}

function readNumbers(form) {
  const weight = parseNumber(form.elements.weight.value);
  const data = { weight, deliveryType: form.elements.deliveryType.value };
  const errors = {};
  if (!Number.isFinite(Math.fround(weight)) || Math.fround(weight) <= 0) {
    errors.weight = "Введіть вагу більше 0 кг.";
  }
  if (!Object.hasOwn(deliveryNames, data.deliveryType)) errors.deliveryType = "Виберіть тип доставки.";
  for (const name of ["depth", "width", "height"]) {
    const text = form.elements[name].value.trim();
    const value = Number(text);
    data[name] = value;
    if (!/^\d+$/.test(text) || !Number.isInteger(value) || value < 1 || value > 2147483647) {
      errors[name] = "Введіть ціле число більше 0.";
    }
  }
  return { data, errors };
}

async function getQuote(data, signal) {
  return request("/postings/quote?" + new URLSearchParams(data), { signal });
}

async function renderEditor(id, signal) {
  const form = content.querySelector("form");
  const title = content.querySelector("[data-title]");
  const loading = content.querySelector("[data-loading]");
  const error = content.querySelector("[data-error]");
  const price = form.querySelector("[data-price]");
  const billable = form.querySelector("[data-billable]");
  const quoteNote = form.querySelector("[data-quote-note]");
  const save = form.querySelector("button[type=submit]");
  let existingValue = 0;
  let saving = false;
  let attempted = false;
  let quoteController;
  let quoteTimer;

  function setError(name, text = "") {
    const control = form.elements[name];
    const fieldError = form.querySelector("#" + name + "-error");
    if (!control || !fieldError) return;
    fieldError.textContent = text;
    if (text) control.setAttribute("aria-invalid", "true");
    else control.removeAttribute("aria-invalid");
  }

  function readFields() {
    const { data, errors } = readNumbers(form);
    for (const [name, message] of [
      ["from", "Вкажіть відправника."], ["to", "Вкажіть отримувача."], ["content", "Опишіть вміст відправлення."]
    ]) {
      data[name] = form.elements[name].value.trim();
      if (!data[name]) errors[name] = message;
    }
    return { data, errors };
  }

  function updateQuote() {
    clearTimeout(quoteTimer);
    quoteController?.abort();
    quoteController = new AbortController();
    const current = quoteController.signal;
    price.textContent = billable.textContent = "—";
    show(quoteNote);
    const { data, errors } = readNumbers(form);
    if (Object.keys(errors).length) return;
    quoteTimer = setTimeout(async () => {
      try {
        const result = await getQuote(data, current);
        if (current.aborted || signal.aborted) return;
        price.textContent = priceLabel(result.price);
        billable.textContent = weightLabel(result.billableWeight);
      } catch (failure) {
        if (current.aborted || signal.aborted) return;
        show(quoteNote, failure.status === 400
          ? "Вага завелика для розрахунку вартості."
          : "Не вдалося розрахувати вартість. " + errorText(failure));
      }
    }, 200);
  }

  signal.addEventListener("abort", () => {
    clearTimeout(quoteTimer);
    quoteController?.abort();
  }, { once: true });
  form.addEventListener("input", event => {
    const name = event.target.name;
    if (attempted) setError(name, readFields().errors[name]);
    if (numericFields.includes(name) || name === "deliveryType") updateQuote();
  });
  form.addEventListener("submit", async event => {
    event.preventDefault();
    if (saving || save.disabled) return;
    attempted = true;
    show(error);
    const { data, errors } = readFields();
    const names = ["from", "to", "content", "deliveryType", ...numericFields];
    names.forEach(name => setError(name, errors[name]));
    const firstError = names.find(name => errors[name]);
    if (firstError) {
      form.elements[firstError].focus();
      return;
    }

    saving = true;
    save.disabled = true;
    save.textContent = "Збереження…";
    const controls = [...form.elements].filter(control => control !== save);
    controls.forEach(control => { control.disabled = true; });
    let sent = false;
    let uncertain = false;
    try {
      const quote = Object.fromEntries([...numericFields, "deliveryType"].map(name => [name, data[name]]));
      await getQuote(quote, signal);
      if (signal.aborted) return;
      sent = true;
      const saved = await request(id === null ? "/postings" : "/postings/" + id, {
        method: id === null ? "POST" : "PUT",
        body: { ...data, value: existingValue }
      });
      if (signal.aborted) {
        if (location.hash === "#/shipments") renderRoute();
        return;
      }
      selectedId = saved.id;
      openList(id === null ? "Відправлення створено." : "Зміни збережено.");
    } catch (failure) {
      if (signal.aborted) return;
      if (!sent && failure.status === 400) {
        setError("weight", "Вага завелика для розрахунку вартості.");
      }
      for (const [name, messages] of Object.entries(failure.fields ?? {})) {
        const fieldName = names.find(field => field.toLowerCase() === name.toLowerCase());
        if (fieldName) setError(fieldName, Array.isArray(messages) ? messages.join(" ") : String(messages));
      }
      uncertain = sent && (!failure.status || failure.status >= 500);
      const advice = uncertain ? " Поверніться до списку, щоб перевірити результат." : "";
      show(error, (sent ? "Не вдалося підтвердити збереження. " : "Не вдалося перевірити вартість. ") +
        errorText(failure) + advice);
    } finally {
      saving = false;
      save.disabled = uncertain;
      save.textContent = "Зберегти";
      controls.forEach(control => { control.disabled = false; });
    }
  });

  if (id === null) return;
  title.textContent = "Редагування відправлення № " + id;
  form.hidden = true;
  loading.hidden = false;
  try {
    const posting = await request("/postings/" + id, { signal });
    if (signal.aborted) return;
    for (const name of ["from", "to", "content", "deliveryType", ...numericFields]) {
      form.elements[name].value = posting[name] ?? "";
    }
    existingValue = posting.value;
    form.hidden = false;
    updateQuote();
  } catch (failure) {
    if (!signal.aborted) show(error, "Не вдалося відкрити відправлення. " + errorText(failure));
  } finally {
    loading.hidden = true;
  }
}

async function renderDetails(id, signal) {
  const loading = content.querySelector("[data-loading]");
  const details = content.querySelector(".details");
  const error = content.querySelector("[data-error]");
  content.querySelector("[data-title]").textContent = "Деталі відправлення № " + id;
  try {
    const posting = await request("/postings/" + id, { signal });
    if (signal.aborted) return;
    appendDetails(details, [
      ["Відправник", posting.from], ["Отримувач", posting.to], ["Вміст", posting.content],
      ["Тип доставки", deliveryLabel(posting.deliveryType)], ["Фактична вага", weightLabel(posting.weight)],
      ["Габарити", [posting.depth, posting.width, posting.height].join(" × ") + " см"],
      ["Дата створення", new Date(posting.createdAt).toLocaleDateString("uk-UA")],
      ["Вартість доставки", priceLabel(posting.price)]
    ]);
    details.hidden = false;
  } catch (failure) {
    if (!signal.aborted) show(error, "Не вдалося завантажити деталі. " + errorText(failure));
  } finally {
    loading.hidden = true;
  }
}

document.querySelector(".skip").addEventListener("click", event => {
  event.preventDefault();
  content.focus();
});
window.addEventListener("hashchange", renderRoute);
renderRoute();
