using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

public class DatabaseHandler
{
    private static string _connectionString = "server=localhost;database=test;uid=root;pwd='';";

    // Statikus függvény, amely végrehajtja az SQL utasítást és megjeleníti az eredményt egy új formban, ha van eredmény
    public static bool ExecuteAndDisplay(string sqlQuery)
    {

        bool res = false;
        try
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) // Ellenőrzi, hogy van-e legalább egy sor eredmény
                        {
                            // Eredmény begyűjtése egy string formájában
                            string result = "";

                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    result += reader.GetName(i) + ": " + reader.GetValue(i).ToString() + "\n";
                                }
                                result += "\n";
                            }

                            Debug.WriteLine("----------------" + sqlQuery + "---------------");
                            // Új form megnyitása az eredményekkel
                            // ResultForm resultForm = new ResultForm(result);
                            //FormUtilities.CenterForm(resultForm);
                            //resultForm.ShowDialog(); // Modal form megjelenítése

                            res = true;
                        }
                        else
                        {
                            MessageBox.Show("Nincs eredmény a lekérdezéshez.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Hiba történt: " + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return res;
    }
}

// Az új form, amely megjeleníti az SQL lekérdezés eredményét
public class ResultForm : Form
{
    private TextBox resultTextBox;

    public ResultForm(string resultData)
    {
        this.Text = "Lekérdezés Eredménye";
        this.Size = new System.Drawing.Size(400, 300);

        resultTextBox = new TextBox();
        resultTextBox.Multiline = true;
        resultTextBox.Dock = DockStyle.Fill;
        resultTextBox.Text = resultData;
        resultTextBox.ReadOnly = true;

        this.Controls.Add(resultTextBox);
    }
}
