using ApiHelper.Models;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiHelper

{
    public class DogApiProcessor
    {

        public static async Task<ObservableCollection<string>> LoadBreedsList()
        {
            ///TODO : À compléter LoadBreedList
            /// Attention le type de retour n'est pas nécessairement bon
            /// J'ai mis quelque chose pour avoir une base
            /// TODO : Compléter le modèle manquant
            /// 

            string url = "https://dog.ceo/api/breeds/list/all";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ObservableCollection<string> breeds = new ObservableCollection<string>();
                    DogBreedsModel result = await response.Content.ReadAsAsync<DogBreedsModel>();

                    foreach(string breed in result.Breeds.Keys)
                    {
                        breeds.Add(breed);
                        if (result.Breeds[breed].Count > 0)
                        {
                            foreach(string sb in result.Breeds[breed])
                            {
                                breeds.Add(breed + '/' + sb);
                            }
                        }
                    }

                    return breeds;


                    //var enfant = result.Families.Values.ToList();
                    //var familles = result.Families.Keys.ToList();
                    //return familles;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<DogResultModel> GetImageUrl(string breed)
        {
            /// TODO : GetImageUrl()
            /// TODO : Compléter le modèle manquant
            /// 
            string url =
                "https://dog.ceo/api/breed/"+breed+"/images/random";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    DogResultModel result = await response.Content.ReadAsAsync<DogResultModel>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
    }
}
