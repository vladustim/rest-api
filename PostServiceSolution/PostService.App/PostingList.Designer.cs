namespace PostService.App;

partial class PostingList
{
    private Label lblTitle = null!;
    private DataGridView gridPostings = null!;
    private Label lblStatus = null!;
    private Button btnNew = null!;
    private Button btnEdit = null!;
    private Button btnDelete = null!;
    private Button btnRefresh = null!;
    private DataGridViewTextBoxColumn colId = null!;
    private DataGridViewTextBoxColumn colFrom = null!;
    private DataGridViewTextBoxColumn colTo = null!;
    private DataGridViewTextBoxColumn colDeliveryType = null!;
    private DataGridViewTextBoxColumn colWeight = null!;
    private DataGridViewTextBoxColumn colPrice = null!;

    private void InitializeComponent()
    {
        lblTitle = new Label();
        gridPostings = new DataGridView();
        lblStatus = new Label();
        btnNew = new Button();
        btnEdit = new Button();
        btnDelete = new Button();
        btnRefresh = new Button();
        colId = new DataGridViewTextBoxColumn();
        colFrom = new DataGridViewTextBoxColumn();
        colTo = new DataGridViewTextBoxColumn();
        colDeliveryType = new DataGridViewTextBoxColumn();
        colWeight = new DataGridViewTextBoxColumn();
        colPrice = new DataGridViewTextBoxColumn();
        SuspendLayout();

        ((System.ComponentModel.ISupportInitialize)gridPostings).BeginInit();

        lblTitle.Name = "lblTitle";
        lblTitle.Location = new Point(24, 23);
        lblTitle.Size = new Size(690, 40);
        lblTitle.Text = "Список поштових відправлень";
        lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(20, 47, 101);

        btnNew.Name = "btnNew";
        btnNew.Location = new Point(24, 79);
        btnNew.Size = new Size(168, 36);
        btnNew.TabIndex = 0;
        btnNew.Text = "Нове відправлення";
        btnNew.BackColor = Color.FromArgb(35, 86, 197);
        btnNew.ForeColor = Color.White;
        btnNew.FlatStyle = FlatStyle.Flat;
        btnNew.FlatAppearance.BorderSize = 0;
        btnNew.Click += btnNew_Click;

        btnEdit.Name = "btnEdit";
        btnEdit.Location = new Point(204, 79);
        btnEdit.Size = new Size(118, 36);
        btnEdit.TabIndex = 1;
        btnEdit.Text = "Редагувати";
        btnEdit.UseVisualStyleBackColor = true;
        btnEdit.Click += btnEdit_Click;

        btnDelete.Name = "btnDelete";
        btnDelete.Location = new Point(334, 79);
        btnDelete.Size = new Size(118, 36);
        btnDelete.TabIndex = 2;
        btnDelete.Text = "Видалити";
        btnDelete.BackColor = Color.FromArgb(177, 36, 40);
        btnDelete.ForeColor = Color.White;
        btnDelete.FlatStyle = FlatStyle.Flat;
        btnDelete.FlatAppearance.BorderSize = 0;
        btnDelete.Click += btnDelete_Click;

        btnRefresh.Name = "btnRefresh";
        btnRefresh.Location = new Point(464, 79);
        btnRefresh.Size = new Size(104, 36);
        btnRefresh.TabIndex = 3;
        btnRefresh.Text = "Оновити";
        btnRefresh.UseVisualStyleBackColor = true;
        btnRefresh.Click += btnRefresh_Click;

        colId.Name = "colId";
        colId.DataPropertyName = "Id";
        colId.HeaderText = "Номер";
        colId.MinimumWidth = 80;
        colId.FillWeight = 12;

        colFrom.Name = "colFrom";
        colFrom.DataPropertyName = "From";
        colFrom.HeaderText = "Відправник";
        colFrom.MinimumWidth = 140;
        colFrom.FillWeight = 29;

        colTo.Name = "colTo";
        colTo.DataPropertyName = "To";
        colTo.HeaderText = "Отримувач";
        colTo.MinimumWidth = 140;
        colTo.FillWeight = 29;

        colDeliveryType.Name = "colDeliveryType";
        colDeliveryType.DataPropertyName = "DeliveryType";
        colDeliveryType.HeaderText = "Тип доставки";
        colDeliveryType.MinimumWidth = 130;
        colDeliveryType.FillWeight = 20;

        colWeight.Name = "colWeight";
        colWeight.DataPropertyName = "Weight";
        colWeight.HeaderText = "Вага, кг";
        colWeight.MinimumWidth = 76;
        colWeight.FillWeight = 10;
        colWeight.DefaultCellStyle.Format = "0.###";

        colPrice.Name = "colPrice";
        colPrice.DataPropertyName = "Price";
        colPrice.HeaderText = "Вартість, грн";
        colPrice.MinimumWidth = 106;
        colPrice.FillWeight = 13;
        colPrice.DefaultCellStyle.Format = "N2";

        gridPostings.Name = "gridPostings";
        gridPostings.Location = new Point(24, 134);
        gridPostings.Size = new Size(1000, 390);
        gridPostings.TabIndex = 4;
        gridPostings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridPostings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridPostings.AllowUserToAddRows = false;
        gridPostings.AllowUserToDeleteRows = false;
        gridPostings.AllowUserToResizeRows = false;
        gridPostings.BackgroundColor = Color.White;
        gridPostings.BorderStyle = BorderStyle.FixedSingle;
        gridPostings.GridColor = Color.FromArgb(218, 227, 240);
        gridPostings.EnableHeadersVisualStyles = false;
        gridPostings.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 238, 249);
        gridPostings.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(20, 47, 101);
        gridPostings.ColumnHeadersHeight = 36;
        gridPostings.DefaultCellStyle.SelectionBackColor = Color.FromArgb(218, 232, 255);
        gridPostings.DefaultCellStyle.SelectionForeColor = Color.FromArgb(20, 47, 101);
        gridPostings.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 250, 254);
        gridPostings.RowTemplate.Height = 33;
        gridPostings.ReadOnly = true;
        gridPostings.RowHeadersVisible = false;
        gridPostings.MultiSelect = false;
        gridPostings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridPostings.Columns.AddRange(new DataGridViewColumn[] { colId, colFrom, colTo, colDeliveryType, colWeight, colPrice });
        gridPostings.SelectionChanged += gridPostings_SelectionChanged;
        gridPostings.CellFormatting += gridPostings_CellFormatting;

        lblStatus.Name = "lblStatus";
        lblStatus.Location = new Point(24, 538);
        lblStatus.Size = new Size(1000, 28);
        lblStatus.Text = "";
        lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblStatus.ForeColor = Color.FromArgb(20, 47, 101);

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(242, 246, 251);
        ClientSize = new Size(1048, 581);
        MinimumSize = new Size(800, 430);
        Name = "PostingList";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "PostService — Облік поштових відправлень";
        Controls.Add(lblTitle);
        Controls.Add(btnNew);
        Controls.Add(btnEdit);
        Controls.Add(btnDelete);
        Controls.Add(btnRefresh);
        Controls.Add(gridPostings);
        Controls.Add(lblStatus);
        Load += PostingList_Load;
        ((System.ComponentModel.ISupportInitialize)gridPostings).EndInit();
        ResumeLayout(false);
    }
}
