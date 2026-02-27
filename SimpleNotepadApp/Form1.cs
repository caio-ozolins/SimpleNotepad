namespace SimpleNotepadApp;

public partial class Form1 : Form
{
    private TextBox caixaTexto = null!;
    private ToolStripStatusLabel labelStatus = null!;

    public Form1()
    {
        InitializeComponent();
        ConfigurarJanela();
    }

    private void ConfigurarJanela()
    {
        this.Text = "Meu Bloco de Notas C#";
        this.Size = new Size(800, 600);

        // 1. Barra de Status
        StatusStrip barraStatus = new StatusStrip();
        labelStatus = new ToolStripStatusLabel();
        labelStatus.Text = "Linha: 1 | Caracteres: 0 | Palavras: 0";
        barraStatus.Items.Add(labelStatus);
        this.Controls.Add(barraStatus);

        // 2. Caixa de Texto
        caixaTexto = new TextBox();
        caixaTexto.Multiline = true;
        caixaTexto.Dock = DockStyle.Fill;
        caixaTexto.ScrollBars = ScrollBars.Vertical;
        caixaTexto.Font = new Font("Segoe UI", 12);
        
        // --- A MÁGICA DO TAB AQUI ---
        caixaTexto.AcceptsTab = true; // Permite que a tecla Tab insira espaços no texto
        // ----------------------------

        // Eventos
        caixaTexto.TextChanged += AtualizarContadores;
        caixaTexto.KeyUp += AtualizarContadores;
        caixaTexto.Click += AtualizarContadores;
        caixaTexto.KeyDown += TratarAtalhosTeclado;
        
        this.Controls.Add(caixaTexto);

        // 3. Menu
        MenuStrip menu = new MenuStrip();
        ToolStripMenuItem menuArquivo = new ToolStripMenuItem("Arquivo");
        ToolStripMenuItem itemAbrir = new ToolStripMenuItem("Abrir", null, AbrirArquivo);
        ToolStripMenuItem itemSalvar = new ToolStripMenuItem("Salvar", null, SalvarArquivo);
        ToolStripMenuItem itemSair = new ToolStripMenuItem("Sair", null, (s, e) => Application.Exit());

        itemAbrir.ShortcutKeys = Keys.Control | Keys.O;
        itemSalvar.ShortcutKeys = Keys.Control | Keys.S;

        menuArquivo.DropDownItems.Add(itemAbrir);
        menuArquivo.DropDownItems.Add(itemSalvar);
        menuArquivo.DropDownItems.Add(new ToolStripSeparator());
        menuArquivo.DropDownItems.Add(itemSair);

        menu.Items.Add(menuArquivo);
        this.MainMenuStrip = menu;
        this.Controls.Add(menu);

        caixaTexto.BringToFront();
    }

    private void TratarAtalhosTeclado(object? sender, KeyEventArgs e)
    {
        // Ctrl + Seta para Baixo
        if (e.Control && e.KeyCode == Keys.Down)
        {
            int indexAtual = caixaTexto.SelectionStart;
            int linhaAtual = caixaTexto.GetLineFromCharIndex(indexAtual);
            int totalLinhas = caixaTexto.Lines.Length;

            if (linhaAtual < totalLinhas - 1)
            {
                int proximaLinhaIndex = caixaTexto.GetFirstCharIndexFromLine(linhaAtual + 1);
                caixaTexto.SelectionStart = proximaLinhaIndex;
            }
            else
            {
                caixaTexto.SelectionStart = caixaTexto.Text.Length;
            }

            caixaTexto.SelectionLength = 0;
            e.SuppressKeyPress = true;
            AtualizarContadores(sender, e);
        }
        // Ctrl + Seta para Cima
        else if (e.Control && e.KeyCode == Keys.Up)
        {
            int indexAtual = caixaTexto.SelectionStart;
            int linhaAtual = caixaTexto.GetLineFromCharIndex(indexAtual);

            if (linhaAtual > 0)
            {
                int linhaAnteriorIndex = caixaTexto.GetFirstCharIndexFromLine(linhaAtual - 1);
                caixaTexto.SelectionStart = linhaAnteriorIndex;
            }
            else
            {
                caixaTexto.SelectionStart = 0;
            }

            caixaTexto.SelectionLength = 0;
            e.SuppressKeyPress = true;
            AtualizarContadores(sender, e);
        }
    }

    private void AtualizarContadores(object? sender, EventArgs e)
    {
        int index = caixaTexto.SelectionStart;
        int linha = caixaTexto.GetLineFromCharIndex(index) + 1;
        int caracteres = caixaTexto.Text.Length;
        
        string texto = caixaTexto.Text.Trim();
        int palavras = 0;
        if (!string.IsNullOrEmpty(texto))
        {
            palavras = texto.Split(new char[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        labelStatus.Text = $"Linha: {linha} | Caracteres: {caracteres} | Palavras: {palavras}";
    }

    private void SalvarArquivo(object? sender, EventArgs e)
    {
        using (SaveFileDialog sfd = new SaveFileDialog())
        {
            sfd.Filter = "Arquivos de Texto|*.txt|Todos os arquivos|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, caixaTexto.Text);
                MessageBox.Show("Arquivo salvo com sucesso!", "Sucesso");
            }
        }
    }

    private void AbrirArquivo(object? sender, EventArgs e)
    {
        using (OpenFileDialog ofd = new OpenFileDialog())
        {
            ofd.Filter = "Arquivos de Texto|*.txt|Todos os arquivos|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                caixaTexto.Text = File.ReadAllText(ofd.FileName);
                this.Text = $"Meu Bloco de Notas C# - {Path.GetFileName(ofd.FileName)}";
            }
        }
    }
}