using System.ComponentModel;

namespace epugaT2.Views;

public partial class FrmCalificacion : ContentPage
{
	public FrmCalificacion()
	{
		InitializeComponent();
	}

    private async void btnCalcularNotas_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Validar nombre
            if (pckNombres.SelectedIndex == -1)
            {
                await DisplayAlert("Validación", "Por favor, seleccione un nombre.", "Aceptar");
                return;
            }

            string nombre = pckNombres.Items[pckNombres.SelectedIndex].ToString();

            // Validar notas
            if (!ValidarCampoNumerico(txtNotaSeguimiento1.Text, out decimal nota1) ||
                !ValidarCampoNumerico(txtExamen1.Text, out decimal examen1) ||
                !ValidarCampoNumerico(txtNotaSeguimiento2.Text, out decimal nota2) ||
                !ValidarCampoNumerico(txtExamen2.Text, out decimal examen2))
            {
                await DisplayAlert("Validación", "Todas las notas son requeridas y deben ser números válidos con hasta dos decimales.", "Aceptar");
                return;
            }

            // Validar rango de notas (opcional)
            if (nota1 > 10 || examen1 > 10 || nota2 > 10 || examen2 > 10 ||
                nota1 < 0 || examen1 < 0 || nota2 < 0 || examen2 < 0)
            {
                await DisplayAlert("Validación", "Las notas deben estar entre 0 y 10.", "Aceptar");
                return;
            }

            // Cálculos con decimales
            decimal promedioParcial1 = nota1 * 0.3m;
            decimal promedioExamen1 = examen1 * 0.2m;
            decimal notaParcial1 = promedioParcial1 + promedioExamen1;

            decimal promedioParcial2 = nota2 * 0.3m;
            decimal promedioExamen2 = examen2 * 0.2m;
            decimal notaParcial2 = promedioParcial2 + promedioExamen2;

            decimal notaFinal = notaParcial1 + notaParcial2;
            string fecha = dtaFecha.Date.ToString("dd/MM/yyyy");

            string estado = notaFinal >= 7 ? "APROBADO"
                            : notaFinal >= 5 ? "COMPLEMENTARIO"
                            : "REPROBADO";

            // Mostrar resultado con dos decimales
            await DisplayAlert("CÁLCULO DE NOTAS",
                $"Nombre: {nombre}\n" +
                $"Fecha: {fecha}\n" +
                $"Nota Parcial 1: {notaParcial1:F2}\n" +
                $"Nota Parcial 2: {notaParcial2:F2}\n" +
                $"Nota Final: {notaFinal:F2}\n" +
                $"Estado: {estado}",
                "Aceptar");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Aceptar");
        }
    }

    // Validar número con máximo 2 decimales
    private bool ValidarCampoNumerico(string texto, out decimal valor)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            valor = 0;
            return false;
        }

        bool esValido = decimal.TryParse(texto, out valor) &&
                        System.Text.RegularExpressions.Regex.IsMatch(texto, @"^\d+(\.\d{1,2})?$");

        return esValido;
    }


}