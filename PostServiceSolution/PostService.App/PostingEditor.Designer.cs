namespace PostService.App;

partial class PostingEditor
{
    private Panel pnlFields = null!;
    private Label lblTitle = null!;
    private Label lblPriceLabel = null!;
    private Label lblPrice = null!;
    private Label lblFeedback = null!;
    private Button btnSave = null!;
    private Button btnCancel = null!;
    private ErrorProvider errors = null!;
    private Label lblFrom = null!;
    private TextBox txtFrom = null!;
    private Label lblTo = null!;
    private TextBox txtTo = null!;
    private Label lblContent = null!;
    private TextBox txtContent = null!;
    private Label lblDelivery = null!;
    private ComboBox cmbDelivery = null!;
    private Label lblWeight = null!;
    private TextBox txtWeight = null!;
    private Label lblDepth = null!;
    private TextBox txtDepth = null!;
    private Label lblWidth = null!;
    private TextBox txtWidth = null!;
    private Label lblHeight = null!;
    private TextBox txtHeight = null!;
    private Label lblBillableWeightLabel = null!;
    private Label lblBillableWeight = null!;

    private System.ComponentModel.IContainer components = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        errors = new ErrorProvider(components);
        pnlFields = new Panel();
        lblTitle = new Label();
        lblPriceLabel = new Label();
        lblPrice = new Label();
        lblFeedback = new Label();
        btnSave = new Button();
        btnCancel = new Button();
        lblFrom = new Label();
        txtFrom = new TextBox();
        lblTo = new Label();
        txtTo = new TextBox();
        lblContent = new Label();
        txtContent = new TextBox();
        lblDelivery = new Label();
        cmbDelivery = new ComboBox();
        lblWeight = new Label();
        txtWeight = new TextBox();
        lblDepth = new Label();
        txtDepth = new TextBox();
        lblWidth = new Label();
        txtWidth = new TextBox();
        lblHeight = new Label();
        txtHeight = new TextBox();
        lblBillableWeightLabel = new Label();
        lblBillableWeight = new Label();
        SuspendLayout();

        pnlFields.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)errors).BeginInit();

        lblTitle.Name = "lblTitle";
        lblTitle.Location = new Point(24, 17);
        lblTitle.Size = new Size(790, 42);
        lblTitle.Text = "Нове відправлення";
        lblTitle.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(20, 47, 101);

        pnlFields.Name = "pnlFields";
        pnlFields.Location = new Point(24, 72);
        pnlFields.Size = new Size(811, 320);
        pnlFields.TabIndex = 0;
        pnlFields.BackColor = Color.White;

        lblFrom.Name = "lblFrom";
        lblFrom.Location = new Point(16, 12);
        lblFrom.Size = new Size(345, 21);
        lblFrom.Text = "Відправник *";

        txtFrom.Name = "txtFrom";
        txtFrom.Location = new Point(16, 35);
        txtFrom.Size = new Size(345, 27);
        txtFrom.TabIndex = 0;
        txtFrom.AccessibleName = "Відправник";

        lblTo.Name = "lblTo";
        lblTo.Location = new Point(430, 12);
        lblTo.Size = new Size(345, 21);
        lblTo.Text = "Отримувач *";

        txtTo.Name = "txtTo";
        txtTo.Location = new Point(430, 35);
        txtTo.Size = new Size(345, 27);
        txtTo.TabIndex = 1;
        txtTo.AccessibleName = "Отримувач";

        lblContent.Name = "lblContent";
        lblContent.Location = new Point(16, 80);
        lblContent.Size = new Size(759, 21);
        lblContent.Text = "Вміст *";

        txtContent.Name = "txtContent";
        txtContent.Location = new Point(16, 103);
        txtContent.Size = new Size(759, 60);
        txtContent.TabIndex = 2;
        txtContent.Multiline = true;
        txtContent.ScrollBars = ScrollBars.Vertical;
        txtContent.AccessibleName = "Вміст";

        lblDelivery.Name = "lblDelivery";
        lblDelivery.Location = new Point(16, 178);
        lblDelivery.Size = new Size(345, 21);
        lblDelivery.Text = "Тип доставки *";

        cmbDelivery.Name = "cmbDelivery";
        cmbDelivery.Location = new Point(16, 201);
        cmbDelivery.Size = new Size(345, 27);
        cmbDelivery.TabIndex = 3;
        cmbDelivery.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbDelivery.AccessibleName = "Тип доставки";

        lblWeight.Name = "lblWeight";
        lblWeight.Location = new Point(430, 178);
        lblWeight.Size = new Size(345, 21);
        lblWeight.Text = "Вага, кг *";

        txtWeight.Name = "txtWeight";
        txtWeight.Location = new Point(430, 201);
        txtWeight.Size = new Size(345, 27);
        txtWeight.TabIndex = 4;
        txtWeight.AccessibleName = "Вага, кг";

        lblDepth.Name = "lblDepth";
        lblDepth.Location = new Point(16, 246);
        lblDepth.Size = new Size(95, 21);
        lblDepth.Text = "Довжина, см *";

        txtDepth.Name = "txtDepth";
        txtDepth.Location = new Point(16, 269);
        txtDepth.Size = new Size(95, 27);
        txtDepth.TabIndex = 5;
        txtDepth.AccessibleName = "Довжина, см";

        lblWidth.Name = "lblWidth";
        lblWidth.Location = new Point(143, 246);
        lblWidth.Size = new Size(95, 21);
        lblWidth.Text = "Ширина, см *";

        txtWidth.Name = "txtWidth";
        txtWidth.Location = new Point(143, 269);
        txtWidth.Size = new Size(95, 27);
        txtWidth.TabIndex = 6;
        txtWidth.AccessibleName = "Ширина, см";

        lblHeight.Name = "lblHeight";
        lblHeight.Location = new Point(270, 246);
        lblHeight.Size = new Size(91, 21);
        lblHeight.Text = "Висота, см *";

        txtHeight.Name = "txtHeight";
        txtHeight.Location = new Point(270, 269);
        txtHeight.Size = new Size(91, 27);
        txtHeight.TabIndex = 7;
        txtHeight.AccessibleName = "Висота, см";

        lblBillableWeightLabel.Name = "lblBillableWeightLabel";
        lblBillableWeightLabel.Location = new Point(454, 413);
        lblBillableWeightLabel.Size = new Size(250, 23);
        lblBillableWeightLabel.Text = "Розрахункова вага";
        lblBillableWeightLabel.ForeColor = Color.FromArgb(20, 47, 101);

        lblBillableWeight.Name = "lblBillableWeight";
        lblBillableWeight.Location = new Point(454, 438);
        lblBillableWeight.Size = new Size(250, 30);
        lblBillableWeight.Text = "—";
        lblBillableWeight.ForeColor = Color.FromArgb(20, 47, 101);
        lblBillableWeight.Font = new Font("Segoe UI", 15F, FontStyle.Bold);

        lblPriceLabel.Name = "lblPriceLabel";
        lblPriceLabel.Location = new Point(40, 413);
        lblPriceLabel.Size = new Size(250, 23);
        lblPriceLabel.Text = "Вартість доставки";
        lblPriceLabel.ForeColor = Color.FromArgb(20, 47, 101);

        lblPrice.Name = "lblPrice";
        lblPrice.Location = new Point(40, 438);
        lblPrice.Size = new Size(250, 30);
        lblPrice.Text = "—";
        lblPrice.ForeColor = Color.FromArgb(20, 47, 101);
        lblPrice.Font = new Font("Segoe UI", 15F, FontStyle.Bold);

        lblFeedback.Name = "lblFeedback";
        lblFeedback.Location = new Point(40, 475);
        lblFeedback.Size = new Size(779, 27);
        lblFeedback.Text = "";
        lblFeedback.ForeColor = Color.FromArgb(177, 36, 40);

        btnCancel.Name = "btnCancel";
        btnCancel.Location = new Point(574, 514);
        btnCancel.Size = new Size(120, 36);
        btnCancel.TabIndex = 1;
        btnCancel.Text = "Скасувати";
        btnCancel.UseVisualStyleBackColor = true;

        btnCancel.DialogResult = DialogResult.Cancel;
        btnSave.Name = "btnSave";
        btnSave.Location = new Point(706, 514);
        btnSave.Size = new Size(129, 36);
        btnSave.TabIndex = 2;
        btnSave.Text = "Зберегти";
        btnSave.BackColor = Color.FromArgb(35, 86, 197);
        btnSave.ForeColor = Color.White;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.Click += btnSave_Click;

        errors.ContainerControl = this;
        errors.BlinkStyle = ErrorBlinkStyle.NeverBlink;

        pnlFields.Controls.Add(lblFrom);
        pnlFields.Controls.Add(txtFrom);
        pnlFields.Controls.Add(lblTo);
        pnlFields.Controls.Add(txtTo);
        pnlFields.Controls.Add(lblContent);
        pnlFields.Controls.Add(txtContent);
        pnlFields.Controls.Add(lblDelivery);
        pnlFields.Controls.Add(cmbDelivery);
        pnlFields.Controls.Add(lblWeight);
        pnlFields.Controls.Add(txtWeight);
        pnlFields.Controls.Add(lblDepth);
        pnlFields.Controls.Add(txtDepth);
        pnlFields.Controls.Add(lblWidth);
        pnlFields.Controls.Add(txtWidth);
        pnlFields.Controls.Add(lblHeight);
        pnlFields.Controls.Add(txtHeight);
        AcceptButton = btnSave;
        CancelButton = btnCancel;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(242, 246, 251);
        ClientSize = new Size(860, 574);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "PostingEditor";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Нове відправлення";
        Controls.Add(lblTitle);
        Controls.Add(pnlFields);
        Controls.Add(lblPriceLabel);
        Controls.Add(lblPrice);
        Controls.Add(lblBillableWeightLabel);
        Controls.Add(lblBillableWeight);
        Controls.Add(lblFeedback);
        Controls.Add(btnCancel);
        Controls.Add(btnSave);
        FormClosing += PostingEditor_FormClosing;
        pnlFields.ResumeLayout(false);
        pnlFields.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)errors).EndInit();
        ResumeLayout(false);
    }
}
