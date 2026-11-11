namespace PostService.App;

partial class PostingList
{
    private DataGridView gridPostings = null!;
    private Button btnDelete = null!;
    private Button btnRefresh = null!;
    private DataGridViewTextBoxColumn colId = null!;
    private DataGridViewTextBoxColumn colFrom = null!;
    private DataGridViewTextBoxColumn colTo = null!;
    private DataGridViewTextBoxColumn colContent = null!;
    private DataGridViewTextBoxColumn colDeliveryType = null!;
    private DataGridViewTextBoxColumn colWeight = null!;
    private DataGridViewTextBoxColumn colWidth = null!;
    private DataGridViewTextBoxColumn colHeight = null!;
    private DataGridViewTextBoxColumn colDepth = null!;
    private DataGridViewTextBoxColumn colValue = null!;
    private DataGridViewTextBoxColumn colPrice = null!;
    private DataGridViewTextBoxColumn colCreatedAt = null!;

    private void InitializeComponent()
    {
        gridPostings = new DataGridView();
        btnDelete = new Button();
        btnRefresh = new Button();
        colId = new DataGridViewTextBoxColumn();
        colFrom = new DataGridViewTextBoxColumn();
        colTo = new DataGridViewTextBoxColumn();
        colContent = new DataGridViewTextBoxColumn();
        colDeliveryType = new DataGridViewTextBoxColumn();
        colWeight = new DataGridViewTextBoxColumn();
        colWidth = new DataGridViewTextBoxColumn();
        colHeight = new DataGridViewTextBoxColumn();
        colDepth = new DataGridViewTextBoxColumn();
        colValue = new DataGridViewTextBoxColumn();
        colPrice = new DataGridViewTextBoxColumn();
        colCreatedAt = new DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)gridPostings).BeginInit();
        SuspendLayout();

        colId.Name = "colId";
        colId.DataPropertyName = "Id";
        colId.HeaderText = "Ідентифікатор";
        colId.Width = 60;
        colId.Visible = false;

        colFrom.Name = "colFrom";
        colFrom.DataPropertyName = "From";
        colFrom.HeaderText = "Відправник";
        colFrom.Width = 125;

        colTo.Name = "colTo";
        colTo.DataPropertyName = "To";
        colTo.HeaderText = "Отримувач";
        colTo.Width = 125;

        colContent.Name = "colContent";
        colContent.DataPropertyName = "Content";
        colContent.HeaderText = "Вміст";
        colContent.Width = 155;

        colDeliveryType.Name = "colDeliveryType";
        colDeliveryType.DataPropertyName = "DeliveryType";
        colDeliveryType.HeaderText = "Тип доставки";
        colDeliveryType.Width = 118;

        colWeight.Name = "colWeight";
        colWeight.DataPropertyName = "Weight";
        colWeight.HeaderText = "Вага, кг";
        colWeight.Width = 74;

        colWidth.Name = "colWidth";
        colWidth.DataPropertyName = "Width";
        colWidth.HeaderText = "Ширина";
        colWidth.Width = 67;

        colHeight.Name = "colHeight";
        colHeight.DataPropertyName = "Height";
        colHeight.HeaderText = "Висота";
        colHeight.Width = 65;

        colDepth.Name = "colDepth";
        colDepth.DataPropertyName = "Depth";
        colDepth.HeaderText = "Глибина";
        colDepth.Width = 67;

        colValue.Name = "colValue";
        colValue.DataPropertyName = "Value";
        colValue.HeaderText = "Цінність";
        colValue.Width = 76;

        colPrice.Name = "colPrice";
        colPrice.DataPropertyName = "Price";
        colPrice.HeaderText = "Вартість";
        colPrice.Width = 78;

        colCreatedAt.Name = "colCreatedAt";
        colCreatedAt.DataPropertyName = "CreatedAt";
        colCreatedAt.HeaderText = "Дата створення";
        colCreatedAt.Width = 115;
        colCreatedAt.Visible = false;

        gridPostings.Name = "gridPostings";
        gridPostings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridPostings.Location = new Point(16, 58);
        gridPostings.Size = new Size(970, 426);
        gridPostings.TabIndex = 2;
        gridPostings.ReadOnly = true;
        gridPostings.AllowUserToAddRows = false;
        gridPostings.AllowUserToDeleteRows = false;
        gridPostings.MultiSelect = false;
        gridPostings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridPostings.RowHeadersVisible = false;
        gridPostings.BackgroundColor = SystemColors.Window;
        gridPostings.Columns.AddRange(new DataGridViewColumn[]
        {
            colId, colFrom, colTo, colContent, colDeliveryType, colWeight,
            colWidth, colHeight, colDepth, colValue, colPrice, colCreatedAt
        });

        btnRefresh.Name = "btnRefresh";
        btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefresh.Location = new Point(752, 14);
        btnRefresh.Size = new Size(112, 32);
        btnRefresh.TabIndex = 0;
        btnRefresh.Text = "Оновити";
        btnRefresh.UseVisualStyleBackColor = true;
        btnRefresh.Click += btnRefresh_Click;

        btnDelete.Name = "btnDelete";
        btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnDelete.Location = new Point(874, 14);
        btnDelete.Size = new Size(112, 32);
        btnDelete.TabIndex = 1;
        btnDelete.Text = "Видалити";
        btnDelete.UseVisualStyleBackColor = true;
        btnDelete.Click += btnDelete_Click;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1002, 500);
        MinimumSize = new Size(600, 300);
        StartPosition = FormStartPosition.CenterScreen;
        Name = "PostingList";
        Text = "Поштові відправлення";
        Controls.Add(gridPostings);
        Controls.Add(btnRefresh);
        Controls.Add(btnDelete);
        Load += PostingList_Load;
        ((System.ComponentModel.ISupportInitialize)gridPostings).EndInit();
        ResumeLayout(false);
    }
}
