namespace QueueMessageProcessor.Application.Models;


public class Constants
{
    public static string WRONG_MESSAGE = "Wrong message";
    public static string WRONG_SUBJECT_MESSAGE = "Wrong subject";
    public static string TITLE_VALUE_EVENT_MESSAGE = "Title value event";
    public static string BILLFORCE_EVENT_MESSAGE = "Billforce event";


    public static Dictionary<string, string> billforceEventTypeDictionary = new Dictionary<string, string>
    {
        { "030", "Acuse de recibo" },
        { "031", "Rechazo" },
        { "032", "Recibo del bien" },
        { "033", "Aceptación expresa" },
        { "034", "Aceptación tacita" },
        { "035", "Aval" },
        { "036", "Incripción como título valor ante RADIAM" },
        { "037", "Endoso en propiedad" },
        { "038", "Endoso en gatantía" },
        { "039", "Endoso en procuración" },
        { "040", "Cancelación del endoso" },
        { "041", "Limitación de circulación como título valor" },
        { "042", "Terminación de la limitación para circulación como título valor" },
        { "043", "Mandato" },
        { "044", "Terminación de mandato" },
        { "045", "Pago total o parcial" },
        { "046", "Informe para el pago" },
        { "047", "Endoso con efecto de sesion ordinaria" },
        { "048", "Protesto"},
        { "049", "Transferencia de los derechos económicos" },
        { "050", "Notificación al deudor sobre la transferencia de derechos económicos" },
        { "051", "Pago de la transferencia de los derechos económicos" }
    };

}
