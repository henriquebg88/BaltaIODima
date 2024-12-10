using MudBlazor;

namespace Dima.Web
{
    public static class Configurations
    {
        public const string HttpClientName = "Dima";
        public static string ApiURL { get; set; } = "http://localhost:5139";
        public static MudTheme Theme = new(){
            Typography = new Typography {
                Default = new Default {
                    FontFamily = ["Raleway", "sans-serif"]
                }
            },
            PaletteLight = new PaletteLight {
                Primary = "#1efa2d",
                Secondary = Colors.LightGreen.Darken3,
                Background = Colors.Gray.Lighten4,
                AppbarBackground = "1efa2d",
                AppbarText = Colors.Shades.Black,
                TextPrimary = Colors.Shades.Black,
                PrimaryContrastText = Colors.Shades.Black,
                DrawerText = Colors.Shades.Black,
                DrawerBackground = Colors.Gray.Lighten4
            },
            PaletteDark = new PaletteDark {
                Primary = Colors.Green.Lighten3,
                Secondary = Colors.LightGreen.Darken3,
                AppbarBackground = Colors.LightGreen.Accent3,
                AppbarText = Colors.Shades.Black,
            }
        };
    }
}