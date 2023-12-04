using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientSample
{
    public class Personajes
    {
        public string name { get; set; }
        public string url { get; set; }
        public string[] episode { get; set; }
    }

    class Program
    {
        static HttpClient rickymorty = new HttpClient();

        static async Task Main()
        {
            rickymorty.BaseAddress = new Uri("https://rickandmortyapi.com/api");

            string nombre = "Summer Smith";
            Personajes personaje = await GetPersonajeAsync(nombre);

            if (personaje != null)
            {
                Console.WriteLine($"Episodios con {nombre}:");
                foreach (var episodeUrl in personaje.episode)
                {
                    string episodeNumber = episodeUrl.Substring(episodeUrl.LastIndexOf('/') + 1);
                    Console.WriteLine($"- Episodio {episodeNumber}");
                }
            }
            Console.ReadLine();
        }

        static async Task<Personajes> GetPersonajeAsync(string nombre)
        {
            Personajes personaje = null;
            HttpResponseMessage response = await rickymorty.GetAsync($"character/?name={nombre}");

            if (response.IsSuccessStatusCode)
            {
                personaje = await response.Content.ReadAsAsync<Personajes>();
            }

            return personaje;
        }
    }
}

