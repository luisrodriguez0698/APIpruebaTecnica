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
                message.From.Add(new MailboxAddress("ECTOTEC", $"evelazquez@ectotec.com"));
                message.To.Add(new MailboxAddress($"{data.name}", $"{data.email}"));
                message.Subject = $"Estimado {data.name}";
                var dateddMMMMYYYY = data.date.ToString("dd-MMMM-yyyy");

                var cuerpoHtml = new StringBuilder();
                cuerpoHtml.AppendLine("<html>");
                cuerpoHtml.AppendLine("<head>");
                cuerpoHtml.AppendLine("<style>");
                cuerpoHtml.AppendLine(".conteiner{");
                cuerpoHtml.AppendLine("height: 100%; display: flex; flex-direction: column; justify-content: center; align-items: center;");
                cuerpoHtml.AppendLine("}");
                cuerpoHtml.AppendLine("</style>");
                cuerpoHtml.AppendLine("</head>");
                cuerpoHtml.AppendLine("<body>");
                cuerpoHtml.AppendLine("<div style=\"height: 100%; display: flex; flex-direction: column; align-items: center; justify-content: center;\">" +
                                     "<div style=\"width: 60%; height: 200px; background-color: #1be22c; display: flex; flex-direction: row;\r\n        border-radius: 60px; align-items: center; justify-content: space-between; padding: 10px; margin: 5% auto;\">" +
                                     "<h1 style=\"font-size: 40px; color: white; text-align: left; margin: 30px;\">Green Leaves</h1>" +
                                     "<img width=\"65\" height=\"65\" src=\"https://img.freepik.com/iconos-gratis/hoja_318-336174.jpg\" />" +
                                     "</div>" +
                                     $"<div><h3>Estimado <b>{data.name}</b></h3>  <p>Hemos recibido sus datos y nos pondremos en contacto con usted en la brevedad posible. Enviaremos un correo con información a su cuenta: {data.email}.</p></div>" +
                                     $"<div style=\"text-align: right; width: 60%;\"><b style=\"display: inline-block;\">Atte.</b> <h3 style=\"color: #1be22c;\">Green Leaves</h3> <h3>{data.cityState} a {dateddMMMMYYYY}</h3></div>" +
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
