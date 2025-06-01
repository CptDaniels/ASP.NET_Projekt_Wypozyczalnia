namespace ASP.NET_Projekt_Wypozyczalnia.ViewModels
{
    public class CarIndexViewModel
    {
        public List<CarViewModel> AllCars { get; set; } = new();
        public List<CarPublicViewModel> PublicCars { get; set; } = new();
    }
}
