using AnthillaCore.Repository;
using System;
using System.Net;
using System.Net.Mail;

namespace AnthillaCore.Mail {

    public class Anth_MailVerification {

        private static void BasicInvite() {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("damianozanardi@yahoo.com");

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("fimbulsgone@gmail.com", "hagall17");
            smtp.Host = "smtp.gmail.com";
            smtp.Timeout = 30000;

            mail.To.Add(new MailAddress("damiano.zanardi@anthilla.com"));

            mail.Headers.Add("X-AnthillaGuid", Guid.NewGuid().ToString());
            mail.Headers.Add("X-NewUser", true.ToString());

            mail.Subject = Guid.NewGuid().ToString().Substring(0, 8) + " - Anthilla Invitation";
            mail.IsBodyHtml = true;

            string body =
            "<div style=\"font-family: 'Roboto', serif;color: #3a3a3a; width: 500px; position: relative; left: 50%; margin-left: -250px;\" class=\"font-effect-decaying\">" +
                "<h1 style=\"text-align: center;\">Welcome to Anthilla!</h1>" +
                "<p style=\"text-align: center;\">Read this and accept to continue to Anthilla</p>" +
                "<hr style=\"border: 1px solid #A7BD39; background-color: #A7BD39;\">" +
                "<p style=\"margin-left: 10px; \">You received an invitation</p>" +
                "<p style=\"margin-left: 10px; \">by ... mettere Admin o User o Company ...</p>" +
                "<p style=\"margin-left: 10px; \">To join (temporaneamente) AnthillaSP</p>" +
                "<hr style=\"border: 1px solid #A7BD39; background-color: #A7BD39;\">" +
                "<label style=\"margin-left: 10px; text-transform: uppercase;\">Your information</label>" +
                "<table style=\"margin-left: 10px; margin-bottom:10px;\">" +
                    "<thead>" +
                        "<tr>" +
                            "<th style=\"width: 240px; border: 0px;\">&nbsp;</th>" +
                            "<th style=\"width: 240px; border: 0px;\">&nbsp;</th>" +
                        "</tr>" +
                    "</thead>" +
                    "<tbody style=\"border: 0px;\">" +
                        "<tr>" +
                            "<td style=\"border: 0px; text-transform: uppercase;\">" +
                                "<label>&nbsp</label>" +
                            "</td>" +
                            "<td style=\"border: 0px;\">" +
                                "internal/external user" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style=\"border: 0px; text-transform: uppercase;\">" +
                                "<label>first name</label>" +
                            "</td>" +
                            "<td style=\"border: 0px;\">" +
                                "Giorgio" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style=\"border: 0px; text-transform: uppercase;\">" +
                                "<label>last name</label>" +
                            "</td> " +
                            "<td style=\"border: 0px;\">" +
                                "Rossi" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style=\"border: 0px; text-transform: uppercase;\">" +
                                "<label>email</label>" +
                            "</td> " +
                            "<td style=\"border: 0px;\">" +
                               "giorgio.rossi@anthilla.com" +
                            "</td>" +
                        "</tr>" +
                    "</tbody>" +
                "</table>" +
                "<hr style=\"border: 1px solid #A7BD39; background-color: #A7BD39;\">" +
            "<button style=\"margin: 10px; background-color: #A7BD39;  border: 0px; color: #3a3a3a; padding: 1px 20px;\">Accept</button>" +
            "<a href=\"#\" style=\"margin: 10px; background-color: #A7BD39;  border: 0px; color: #3a3a3a; padding: 1px 20px;\">Accept</a>" +
            "<button style=\"float: right; margin: 10px; background-color: #A7BD39;  border: 0px; color: #3a3a3a; padding: 1px 20px;\">Decline</button>" +
            "<a href=\"#\" style=\"margin: 10px; background-color: #A7BD39;  border: 0px; color: #3a3a3a; padding: 1px 20px;\">Decline</a>" +
            "</div>";

            mail.Body = body;
            smtp.Send(mail);
        }

        private static Anth_VerificationUrlRepository urlepo = new Anth_VerificationUrlRepository();
        private static Anth_UserRepository userRepo = new Anth_UserRepository();

        public static void UserInvite(string _fromGuid, string _fName, string _lName, string _email, bool _insider, string _pack) {
            string newUserGuid = Guid.NewGuid().ToString();
            string fromGuid = _fromGuid;
            string fName = _fName;
            string lName = _lName;
            string email = _email;
            string pack = _pack;
            bool insider = _insider; //true insider; false outsider
            string invitationGuid = Guid.NewGuid().ToString().Substring(0, 8);
            var userFrom = userRepo.GetById(fromGuid);
            string from = userFrom.AnthillaEmail;
            string insType;
            if (insider == true) {
                userRepo.MakeInsider(newUserGuid);
                insType = "internal";
            }
            else {
                userRepo.MakeOutsider(newUserGuid);
                insType = "external";
            }
            userRepo.Create(newUserGuid, fName, lName, invitationGuid, email, "invitation-sent" + insType);

            if (insType == "internal") {
                userRepo.AssignGroup(newUserGuid, "00000000-0000-0000-0000-000000001001");
            }
            else {
                userRepo.AssignGroup(newUserGuid, "00000000-0000-0000-0000-000000001002");
            }

            string downloadGuid = Guid.NewGuid().ToString();
            //string guid, string userGuid, string packageGuid, string emailTo
            urlepo.Create(downloadGuid, newUserGuid, pack, email);

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("fimbulsgone@gmail.com", "hagall17");
            smtp.Host = "smtp.gmail.com";
            smtp.Timeout = 30000;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            //mail.To.Add(new MailAddress("damiano.zanardi@anthilla.com"));
            mail.To.Add(new MailAddress(email));
            mail.Headers.Add("X-InvGuid", invitationGuid);
            mail.Subject = invitationGuid + " - Anthilla Invitation";
            mail.IsBodyHtml = true;
            string body =
            "<div style=\"font-family: 'Roboto', serif;color: #3a3a3a; width: 500px; position: relative; left: 50%; margin-left: -250px;\" class=\"font-effect-decaying\">" +
                "<h1 style=\"text-align: center;\">Welcome to Anthilla!</h1>" +
                "<p style=\"text-align: center;\">Read this and accept to continue to Anthilla</p>" +
                "<hr style=\"border: 1px solid #A7BD39; background-color: #A7BD39;\">" +
                "<p style=\"margin-left: 10px; \">You received an invitation</p>" +
                "<p style=\"margin-left: 10px; \">by " + userFrom.AnthillaFirstName + " " + userFrom.AnthillaLastName + "</p>" +
                "<p style=\"margin-left: 10px; \">To join temporarily AnthillaSP</p>" +
                "<hr style=\"border: 1px solid #A7BD39; background-color: #A7BD39;\">" +
                "<label style=\"margin-left: 10px; text-transform: uppercase;\">Your information</label>" +
                "<table style=\"margin-left: 10px; margin-bottom:10px;\">" +
                    "<thead>" +
                        "<tr>" +
                            "<th style=\"width: 240px; border: 0px;\">&nbsp;</th>" +
                            "<th style=\"width: 240px; border: 0px;\">&nbsp;</th>" +
                        "</tr>" +
                    "</thead>" +
                    "<tbody style=\"border: 0px;\">" +
                        "<tr>" +
                            "<td style=\"border: 0px; text-transform: uppercase;\">" +
                                "<p></p>" +
                            "</td>" +
                            "<td style=\"border: 0px;\">" +
                                insType +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style=\"border: 0px; text-transform: uppercase;\">" +
                                "<label>first name</label>" +
                            "</td>" +
                            "<td style=\"border: 0px;\">" +
                                fName +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style=\"border: 0px; text-transform: uppercase;\">" +
                                "<label>last name</label>" +
                            "</td> " +
                            "<td style=\"border: 0px;\">" +
                                lName +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style=\"border: 0px; text-transform: uppercase;\">" +
                                "<label>email</label>" +
                            "</td> " +
                            "<td style=\"border: 0px;\">" +
                                email +
                            "</td>" +
                        "</tr>" +
                    "</tbody>" +
                "</table>" +
                "<hr style=\"border: 1px solid #A7BD39; background-color: #A7BD39;\">" +
            "<a href=\"http://localhost:8085/download/" + downloadGuid + "\" target=\"_blank\" style=\"margin: 10px; background-color: #A7BD39;  border: 0px; color: #3a3a3a; padding: 1px 20px;\">Accept</a>" +
            "<a href=\"#\" target=\"_blank\" style=\"margin: 10px; margin-left: 45px; background-color: #A7BD39;  border: 0px; color: #3a3a3a; padding: 1px 20px;\">Decline</a>" +
            "</div>";
            mail.Body = body;
            smtp.Send(mail);
        }
    }
}