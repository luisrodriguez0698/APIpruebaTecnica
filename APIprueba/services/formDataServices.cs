using APIprueba.Interfaz;
using APIprueba.models;
using MimeKit;
using MailKit.Net.Smtp;
using System.Text;
using APIprueba.DB;
using Microsoft.EntityFrameworkCore;

namespace APIprueba.services
{
    public class formDataServices : IformData
    {
        private readonly dbConexion dbConexion;

        public formDataServices(dbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }
        public async Task<bool> datos(formsData data)
        {
            var Success = false;

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("LUIS", $"qwerty_eduard98@hotmail.com"));
                message.To.Add(new MailboxAddress($"{data.name}", $"{data.email}"));
                message.Subject = $"Estimado {data.name}";
                var dateddMMMMYYYY = data.date.ToString("dd-MMMM-yyyy");

                var cuerpoHtml = new StringBuilder();
                cuerpoHtml.AppendLine("<html>");
                cuerpoHtml.AppendLine("<head>");
                cuerpoHtml.AppendLine("<style>");
                cuerpoHtml.AppendLine(".conteiner{");
                cuerpoHtml.AppendLine("display: flex; flex-direction: column; align-items: center; justify-content: center; height: 100%;");
                cuerpoHtml.AppendLine("}");
                cuerpoHtml.AppendLine("</style>");
                cuerpoHtml.AppendLine("</head>");
                cuerpoHtml.AppendLine("<body>");
                cuerpoHtml.AppendLine("<div style=\"display: flex; flex-direction: column; align-items: center; justify-content: center; height: 100%;\">" +
                                     "<div style=\"background-color: #1EF531; width: 500px; height: 200px; display: flex; flex-direction: row; align-items: center; justify-content: space-between; border-radius: 50px; margin-bottom: 10%; padding: 10px;\">" +
                                     "<h1 style=\"color: white; font-size: 50px; text-align: left; margin: 30px;\">Green Leaves</h1>" +
                                     "<img width=\"70\" height=\"70\" src=\"https://toppng.com/uploads/thumbnail/clipart-hoja-hoja-de-arbol-logo-11562973259drxvxjjfl6.png\" />" +
                                     "</div>" +
                                     $"<div><h3>Estimado <b>{data.name}</b></h3>  <p>Hemos recibido sus datos y nos pondremos en contacto con usted en la brevedad posible. Enviaremos un correo con información a su cuenta: {data.email}.</p></div>" +
                                     $"<div style=\"text-align: right;\"><b style=\"display: inline-block;\">Atte.</b> <h3 style=\"color: green;\">Green Leaves</h3> <h3>{data.cityState} a {dateddMMMMYYYY}</h3></div>" +
                                     "</div>");
                cuerpoHtml.AppendLine("</body>");
                cuerpoHtml.AppendLine("</html>");

                message.Body = new TextPart("html")
                {
                    Text = cuerpoHtml.ToString()
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.office365.com", 587, false);
                    client.Authenticate("", "");
                    client.Send(message);
                    client.Disconnect(true);
                }
           
                Success = true;
                return Success;
            }
            catch (Exception ex)
            {
                Success = false;
                return Success;
            }
        }

        public async Task<List<Estado>> Estados()   
        {
            try
            {
                return await dbConexion.Estado.ToListAsync();
            }
            catch (Exception)
            {
                return new List<Estado> ();
            }
        }
    }
}
