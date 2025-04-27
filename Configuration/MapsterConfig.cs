using Mapster;
using ASP.NET_Projekt_Wypozyczalnia.Models;
using ASP.NET_Projekt_Wypozyczalnia.ViewModels;

public static class MapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<Client, ClientViewModel>
            .NewConfig()
            .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}")
            .Map(dest => dest.DocumentInfo, src => $"{GetDocumentName(src.DocumentType)} - {src.DocumentNumber}");
    }

    private static string GetDocumentName(DocumentType documentType)
    {
        return documentType switch
        {
            DocumentType.ID => "Dowód osobisty",
            DocumentType.DriverLicence => "Prawo jazdy",
            _ => documentType.ToString()
        };
    }
}