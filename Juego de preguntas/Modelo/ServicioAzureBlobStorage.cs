using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_de_preguntas.Modelo
{

    class ServicioAzureBlobStorage
    {
        public string GuardarImagenEnAzure(string ruta)
        {
            //Paquete NuGet para Azure Storage Blob Service: Azure.Storage.Blobs
            string cadenaConexion = "DefaultEndpointsProtocol=https;AccountName=trivialjusep;AccountKey=KplKyj0pu93OmPlGHMuPOneqCSFu5b8C0LyTr3G4TTEhQMEG+XAhmfnjargute8LS4yD1+eEKRis+ASt6hunQw==;EndpointSuffix=core.windows.net"; //La obtenemos en el portal de Azure, asociada a la cuenta de almacenamiento
            string nombreContenedorBlobs = "trivial"; //El nombre que le hayamos dado a nuestro contenedor de blobs en el portal de Azure
            string rutaImagen = ruta;

            //Obtenemos el cliente del contenedor
            BlobServiceClient clienteBlobService = new BlobServiceClient(cadenaConexion);
            BlobContainerClient clienteContenedor = clienteBlobService.GetBlobContainerClient(nombreContenedorBlobs);

            //Leemos la imagen y la subimos al contenedor
            Stream streamImagen = File.OpenRead(rutaImagen);
            string nombreImagen = Path.GetFileName(rutaImagen);

            if (!clienteContenedor.GetBlobClient(nombreImagen).Exists())
            {
                clienteContenedor.UploadBlob(nombreImagen, streamImagen);
            }
            

            //Una vez subida, obtenemos la URL para referenciarla
            var clienteBlobImagen = clienteContenedor.GetBlobClient(nombreImagen);
            string urlImagen = clienteBlobImagen.Uri.AbsoluteUri;

            return urlImagen;
        }
    }
}
