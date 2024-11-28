using System.Text.Json;
using System.Windows.Forms;
using Models;

namespace InfTech_3_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void startButton_Click(object sender, EventArgs e)
        {
            const string apiUrl = "https://catfact.ninja/fact";

            try
            {
                using HttpClient client = new HttpClient();
                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode(); // Проверка успешности запроса
                string jsonResponse = await response.Content.ReadAsStringAsync();

                var catFact = JsonSerializer.Deserialize<CatFactModel>(jsonResponse);

                if (catFact != null)
                {
                    Label label = new Label
                    {
                        Text = $"Случайный факт о кошках:\n{catFact.fact}",
                        AutoSize = true,
                        MaximumSize = new System.Drawing.Size(652, 200)
                    };

                    // Очистка предыдущих элементов и добавление нового факта
                    panel1.Controls.Clear();
                    panel1.Controls.Add(label);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
