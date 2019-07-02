using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YouSign
{
    public class YouSignService : IYouSignService
    {
        private HttpClient HttpClient { get; }

        #region Methods

        public YouSignService(HttpClient httpClient)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            HttpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            HttpClient.Timeout = new TimeSpan(0, 10, 0);
        }

        /// <summary>
        ///     Permet de référencer un fichier.
        /// </summary>
        /// <param name="input">Le fichier à référencer.</param>
        /// <returns></returns>
        public async Task<FileOutput> FilePostAsync(FileInput input)
        {
            // Prépare la requête.
            using (var request = new HttpRequestMessage(HttpMethod.Post, "files"))
            {
                request.Content =
                    new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

                // Exécute la requête.
                using (var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    // Désérialise la réponse.
                    var stream = await response.Content.ReadAsStreamAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return stream.DeserializeJsonFromStream<FileOutput>();
                    }

                    // Désérialise l'erreur.
                    var error = stream.DeserializeJsonFromStream<ErrorOutput>();
                    throw new ApiException
                    {
                        StatusCode = response.StatusCode,
                        Content = error,
                        Source = "API YouSign",
                        ErrorMessage = $"{error.Title} : {error.Detail}"
                    };
                }
            }
        }

        /// <summary>
        ///     Permet de récupérer les informations d'un document.
        /// </summary>
        /// <param name="id">L'identifiant du fichier /files/guid</param>
        /// <returns></returns>
        public async Task<FileOutput> FileGetAsync(string id)
        {
            // Prépare la requête.
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{id}")
            ) // TODO check url, may be should add files/{id}
            {
                // Exécute la requête.
                using (var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    // Désérialise la réponse.
                    var stream = await response.Content.ReadAsStreamAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return stream.DeserializeJsonFromStream<FileOutput>();
                    }

                    // Désérialise l'erreur.
                    var error = stream.DeserializeJsonFromStream<ErrorOutput>();
                    throw new ApiException
                    {
                        StatusCode = response.StatusCode,
                        Content = error,
                        Source = "API YouSign",
                        ErrorMessage = $"{error.Title} : {error.Detail}"
                    };
                }
            }
        }

        /// <summary>
        ///     Permet de télécharger un fichier.
        /// </summary>
        /// <param name="id">L'identifiant du fichier /files/guid</param>
        /// <returns></returns>
        public async Task<string> FileDownloadAsync(string id)
        {
            // Prépare la requête.
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{id}/download"))
            {
                // Exécute la requête.
                using (var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    // Désérialise la réponse.
                    var stream = await response.Content.ReadAsStreamAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return stream.DeserializeJsonFromStream<string>();
                    }

                    // Désérialise l'erreur.
                    var error = stream.DeserializeJsonFromStream<ErrorOutput>();
                    throw new ApiException
                    {
                        StatusCode = response.StatusCode,
                        Content = error,
                        Source = "API YouSign",
                        ErrorMessage = $"{error.Title} : {error.Detail}"
                    };
                }
            }
        }

        /// <summary>
        ///     Permet de démarrer une nouvelle procédure de signature.
        /// </summary>
        /// <param name="input">La procédure à démarrer.</param>
        /// <returns></returns>
        public async Task<ProcedureOutput> ProcedurePostAsync(ProcedureInput input)
        {
            // Prépare la requête.
            using (var request = new HttpRequestMessage(HttpMethod.Post, "procedures"))
            {
                request.Content =
                    new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

                // Exécute la requête.
                using (var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    // Désérialise la réponse.
                    var stream = await response.Content.ReadAsStreamAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return stream.DeserializeJsonFromStream<ProcedureOutput>();
                    }

                    // Désérialise l'erreur.
                    var error = stream.DeserializeJsonFromStream<ErrorOutput>();
                    throw new ApiException
                    {
                        StatusCode = response.StatusCode,
                        Content = error,
                        Source = "API YouSign",
                        ErrorMessage = $"{error.Title} : {error.Detail}"
                    };
                }
            }
        }

        /// <summary>
        ///     Permet de supprimer un objet à partir de son identifiant.
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAsync(string id)
        {
            // Prépare la requête.
            using (var request = new HttpRequestMessage(HttpMethod.Delete, $"{id}"))
            {
                // Exécute la requête.
                using (var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        // Désérialise l'erreur.
                        var stream = await response.Content.ReadAsStreamAsync();
                        var error = stream.DeserializeJsonFromStream<ErrorOutput>();
                        throw new ApiException
                        {
                            StatusCode = response.StatusCode,
                            Content = error,
                            Source = "API YouSign",
                            ErrorMessage = $"{error.Title} : {error.Detail}"
                        };
                    }
                }
            }
        }

        /// <summary>
        ///     Permet d'envoyer un fichier en cachet serveur.
        /// </summary>
        /// <param name="cachet">Le fichier à cacheter.</param>
        /// <returns></returns>
        public async Task<StampOutput> ServerStampsPostAsync(StampInput cachet)
        {
            // Prépare la requête.
            using (var request = new HttpRequestMessage(HttpMethod.Post, "server_stamps"))
            {
                request.Content =
                    new StringContent(JsonConvert.SerializeObject(cachet), Encoding.UTF8, "application/json");

                // Exécute la requête.
                using (var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    // Désérialise la réponse.
                    var stream = await response.Content.ReadAsStreamAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return stream.DeserializeJsonFromStream<StampOutput>();
                    }

                    // Désérialise l'erreur.
                    var error = stream.DeserializeJsonFromStream<ErrorOutput>();
                    throw new ApiException
                    {
                        StatusCode = response.StatusCode,
                        Content = error,
                        Source = "API YouSign",
                        ErrorMessage = $"{error.Title} : {error.Detail}"
                    };
                }
            }
        }

        /// <summary>
        ///     Permet de lister les personnalisations d'iframe de signature.
        /// </summary>
        /// <returns></returns>
        public async Task<List<SignatureUIOutput>> SignaturesUIGetAsync()
        {
            // Prépare la requête.
            using (var request = new HttpRequestMessage(HttpMethod.Get, "signature_uis"))
            {
                // Exécute la requête.
                using (var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    // Désérialise la réponse.
                    var stream = await response.Content.ReadAsStreamAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var uis = stream.DeserializeJsonFromStream<List<SignatureUIInput>>();
                        return uis.Select(o => new SignatureUIOutput
                        {
                            Id = o.Id, Name = o.Name, Description = o.Description, CreatedAt = o.CreatedAt
                        }).ToList();
                    }

                    // Désérialise l'erreur.
                    var error = stream.DeserializeJsonFromStream<ErrorOutput>();
                    throw new ApiException
                    {
                        StatusCode = response.StatusCode,
                        Content = error,
                        Source = "API YouSign",
                        ErrorMessage = $"{error.Title} : {error.Detail}"
                    };
                }
            }
        }

        /// <summary>
        ///     Permet de créer une personnalisation d'iframe de signature.
        /// </summary>
        /// <param name="input">La personnalisation à créer.</param>
        /// <returns></returns>
        public async Task<SignatureUIInput> SignaturesUIPostAsync(SignatureUIInput input)
        {
            // Prépare la requête.
            using (var request = new HttpRequestMessage(HttpMethod.Post, "signature_uis"))
            {
                request.Content =
                    new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

                // Exécute la requête.
                using (var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    // Désérialise la réponse.
                    var stream = await response.Content.ReadAsStreamAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return stream.DeserializeJsonFromStream<SignatureUIInput>();
                    }

                    // Désérialise l'erreur.
                    var error = stream.DeserializeJsonFromStream<ErrorOutput>();
                    throw new ApiException
                    {
                        StatusCode = response.StatusCode,
                        Content = error,
                        Source = "API YouSign",
                        ErrorMessage = $"{error.Title} : {error.Detail}"
                    };
                }
            }
        }

        public async Task<Layout> GetLayoutAsync(string fileId)
        {
            // Prépare la requête.
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{fileId}/layout"))
            {
                // Exécute la requête.
                using (var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    // Désérialise la réponse.
                    var stream = await response.Content.ReadAsStreamAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return stream.DeserializeJsonFromStream<Layout>();
                    }

                    // Désérialise l'erreur.
                    var error = stream.DeserializeJsonFromStream<ErrorOutput>();
                    throw new ApiException
                    {
                        StatusCode = response.StatusCode,
                        Content = error,
                        Source = "API YouSign",
                        ErrorMessage = $"{error.Title} : {error.Detail}"
                    };
                }
            }
        }

        #endregion
    }
}
