using AnthillaCore.Models;
using AnthillaCore.Repository;
using System;
using System.Collections.Generic;

namespace AnthillaCore {

    public class FillDBCore {
        private Anth_UserRepository userRepo = new Anth_UserRepository();
        private Anth_CompanyRepository compRepo = new Anth_CompanyRepository();
        private Anth_ProjectRepository projRepo = new Anth_ProjectRepository();
        private Anth_UserGroupRepository ugrpRepo = new Anth_UserGroupRepository();
        private Anth_FunctionGroupRepository funcRepo = new Anth_FunctionGroupRepository();

        public Anth_UserModel[] Impressionisti() {
            var userList = new List<Anth_UserModel>();

            var fnames = new string[] {
                "Zacharie",
                "Jean-Frederic",
                "Eugene",
                "Gustave",
                "Giuseppe",
                "Edgar",
                "Pieter Franciscus",
                "Charles",
                "Ivan",
                "Jean-Baptiste",
                "Childe",
                "Robert",
                "Peder Severin",
                "Edouard",
                "Berthe",
                "Camille",
                "Pierre-Auguste",
                "Alfred",
                "Max",
                "Mary",
                "Eliseu",
                "Federico"
	        };

            var lnames = new string[] {
                "Astruc",
                "Bazille",
                "Boudin",
                "Caillebotte",
                "De Nittis",
                "Degas",
                "Dierckx",
                "Gleyre",
                "Grohar",
                "Guillaumin",
                "Hassam",
                "Henri",
                "Kroyer",
                "Manet",
                "Morisot",
                "Pissarro",
                "Renoir",
                "Sisley",
                "Slevogt",
                "Cassatt",
                "Visconti",
                "Zandomeneghi"
	        };

            var password = "ImpressionistiDev2014";

            var emails = new string[] {
                "zacharie.astruc@anthilla.com",
                "jean.frederic.bazille@anthilla.com",
                "eugene.boudin@anthilla.com",
                "gustave.caillebotte@anthilla.com",
                "giuseppe.denittis@anthilla.com",
                "edgar.degas@anthilla.com",
                "pieter.franciscus.dierckx@anthilla.com",
                "charles.gleyre@anthilla.com",
                "ivan.grohar@anthilla.com",
                "jean.baptiste.guillaumin@anthilla.com",
                "childe.hassam@anthilla.com",
                "robert.henri@anthilla.com",
                "peder.severin.kroyer@anthilla.com",
                "edouard.manet@anthilla.com",
                "berthe.morisot@anthilla.com",
                "camille.pissarro@anthilla.com",
                "pierre.auguste.renoir@anthilla.com",
                "alfred.sisley@anthilla.com",
                "max.slevogt@anthilla.com",
                "mary.cassatt@anthilla.com",
                "eliseu.visconti@anthilla.com",
                "federico.zandomeneghi@anthilla.com"
	        };

            var tags = "impressionista,pittore";

            for (int i = 0; i <= fnames.Length - 1; i++) {
                var user = userRepo.Create(Guid.NewGuid().ToString(), fnames[i], lnames[i], password, emails[i], tags);
                userList.Add(user);
            }

            var array = userList.ToArray();
            return array;
        }

        public Anth_UserModel[] Liberty() {
            var userList = new List<Anth_UserModel>();

            var fnames = new string[] {
                "Aubrey",
                "Pierre",
                "William",
                "Walter",
                "Adolfo",
                "Ettore",
                "Eugène",
                "Ferdinand",
                "Adolf",
                "Gustav",
                "Fernand",
                "Melchior",
                "Privat",
                "Giovanni Maria",
                "Frances",
                "Margaret",
                "Alfons",
                "Théophile Alexandre",
                "Aleardo",
                "Jan",
                "Eliseu",
                "Edmund",
                "Marcello",
                "Leopoldo",
                "Archibald"
	        };

            var lnames = new string[] {
                "Beardsley",
                "Bonnard",
                "Bradley",
                "Crane",
                "De Carolis",
                "De Maria Bergler",
                "Grasset",
                "Hodler",
                "Hohenstein",
                "Klimt",
                "Khnopff",
                "Lechte",
                "Livemont",
                "Mataloni",
                "Macdonald",
                "MacDonald",
                "Mucha",
                "Steinlen",
                "Terzi",
                "Toorop",
                "Visconti",
                "Dulac",
                "Dudovich",
                "Metlicovitz",
                "Knox"
	        };

            var password = "LibertyDev2014";

            var emails = new string[] {
                "aubrey.beardsley@anthilla.com",
                "pierre.bonnard@anthilla.com",
                "william.bradley@anthilla.com",
                "walter.crane@anthilla.com",
                "adolfo.decarolis@anthilla.com",
                "ettore.demariabergler@anthilla.com",
                "eugene.grasset@anthilla.com",
                "ferdinand.hodler@anthilla.com",
                "adolf.hohenstein@anthilla.com",
                "gustav.klimt@anthilla.com",
                "fernand.khnopff@anthilla.com",
                "melchior.lechte@anthilla.com",
                "privat.livemont@anthilla.com",
                "giovannimaria.mataloni@anthilla.com",
                "frances.macdonald@anthilla.com",
                "margaret.macdonald@anthilla.com",
                "alfons.mucha@anthilla.com",
                "theophilealexandre.steinlen@anthilla.com",
                "aleardo.terzi@anthilla.com",
                "jan.toorop@anthilla.com",
                "eliseu.visconti@anthilla.com",
                "edmund.dulac@anthilla.com",
                "marcello.dudovich@anthilla.com",
                "leopoldo.metlicovitz@anthilla.com",
                "archibald.knox@anthilla.com"
	        };

            var tags = "impressionista,art nouveau,francia,scuola di nancy";

            for (int i = 0; i <= fnames.Length - 1; i++) {
                var user = userRepo.Create(Guid.NewGuid().ToString(), fnames[i], lnames[i], password, emails[i], tags);
                userList.Add(user);
            }

            var array = userList.ToArray();
            return array;
        }

        public Anth_CompanyModel[] Giocattoli() {
            var compList = new List<Anth_CompanyModel>();

            var aliass = new string[] {
                "Adica Pongo",
                "Atlantic",
                "Cardini",
                "Furga",
                "Gama",
                "Harbert",
                "Ingap",
                "Lenci",
                "Polistil"
            };

            var tags = "classico,giocattoli,vintage";

            var streets = new string[] {
                "via Santa Maria a Castagnolo",
                "corso Buenos Aires",
                "via IV Novembre",
                "piazza Matteotti",
                "Gleissbühlstrasse",
                "via Del Lauro",
                "via Andrea da Faenza",
                "via Gian Domenico Cassini",
                "via Giuseppe Verdi"
            };

            var numbers = new string[] {
                "5",
                "28",
                "294",
                "1",
                "10",
                "7",
                "6",
                "7",
                "8"
            };

            var towns = new string[] {
                "Lastra a Signa (FI)",
                "Milano (MI)",
                "Omegna (VB)",
                "Canneto sull'Oglio (MN)",
                "Norimberga",
                "Milano (MI)",
                "Bologna (MI)",
                "Torino (BO)",
                "Assago (MI)"
            };

            var caps = new string[] {
                "50145",
                "20121",
                "28887",
                "46013",
                "90402",
                "20121",
                "40121",
                "10121",
                "20090"
            };

            var states = new string[] {
                "IT",
                "IT",
                "IT",
                "IT",
                "DE",
                "IT",
                "IT",
                "IT",
                "IT"
            };

            for (int i = 0; i <= aliass.Length - 1; i++) {
                var guid = Guid.NewGuid().ToString();
                var comp = compRepo.Create(guid, aliass[i], tags);
                var comp2 = compRepo.AssignAddress(comp.CompanyGuid, streets[i], numbers[i], towns[i], caps[i], states[i]);
                compList.Add(comp2);
            }

            var array = compList.ToArray();
            return array;
        }

        public Anth_CompanyModel[] Cioccolato() {
            var compList = new List<Anth_CompanyModel>();

            var aliass = new string[] {
                "Caffarel",
                "Elah Dufour",
                "Feletti",
                "La Suissa",
                "Lindt & Sprüngli",
                "Nestlé",
                "Pernigotti",
                "Venchi"
            };

            var tags = "dolciumi,cioccolato";

            var streets = new string[] {
                "via Caduti per la libertà",
                "strada Serravalle",
                "via Cascine",
                "via Serravalle",
                "Seestrasse",
                "via del Mulino",
                "vle della Rimembranza",
                "via Venchi"
            };

            var numbers = new string[] {
                "1",
                "73",
                "32",
                "99",
                "204",
                "6",
                "100",
                "1"
            };

            var towns = new string[] {
                "Luserna San Giovanni (TO)",
                "Novi Ligure (AL)",
                "Pont Saint Martin (AO)",
                "Arquata Scrivia (AL)",
                "Kilchberg",
                "Assago (MI)",
                "Novi Ligure (AL)",
                "Castelletto Stura (CN)"
            };

            var caps = new string[] {
                "10062",
                "15067",
                "11026",
                "15061",
                "8802",
                "20090",
                "15067",
                "12040"
            };

            var states = new string[] {
                "IT",
                "IT",
                "IT",
                "CH",
                "IT",
                "IT",
                "IT",
                "IT"
            };

            for (int i = 0; i <= aliass.Length - 1; i++) {
                var guid = Guid.NewGuid().ToString();
                var comp = compRepo.Create(guid, aliass[i], tags);
                var comp2 = compRepo.AssignAddress(comp.CompanyGuid, streets[i], numbers[i], towns[i], caps[i], states[i]);
                compList.Add(comp2);
            }

            var array = compList.ToArray();
            return array;
        }

        public Anth_ProjectModel[] AereiX() {
            var projList = new List<Anth_ProjectModel>();

            var aliass = new string[] {
                "X-1",
                "X-2 \"Starbuster\"",
                "X-3 Stiletto",
                "X-4 Bantam",
                "X-5",
                "X-6",
                "X-7 \"Flying Stove Pipe\""
            };

            var tags = "aerei-x,progetto,segreto";

            for (int i = 0; i <= aliass.Length - 1; i++) {
                var guid = Guid.NewGuid().ToString();
                var proj = projRepo.Create(guid, aliass[i], tags);
                projList.Add(proj);
            }

            var array = projList.ToArray();
            return array;
        }

        public Anth_ProjectModel[] Invenzioni() {
            var projList = new List<Anth_ProjectModel>();

            var aliass = new string[] {
                "AB Hancer",
                "Baby Mocio",
                "Bacchette per Mescolare",
                "Campanello a Pianoforte",
                "Cappello di Lana con Barba",
                "Microfono da Doccia",
                "Ombrello Pistola ad Acqua",
                "Bici da Corridore"
            };

            var tags = "invenzioni,geniali,einstein,1851";

            for (int i = 0; i <= aliass.Length - 1; i++) {
                var guid = Guid.NewGuid().ToString();
                var proj = projRepo.Create(guid, aliass[i], tags);
                projList.Add(proj);
            }

            var array = projList.ToArray();
            return array;
        }

        public Anth_UserGroupModel[] Gruppi() {
            var ugrpList = new List<Anth_UserGroupModel>();

            var aliass = new string[] {
                "Master",
                "Admin",
                "Power",
                "Support",
                "Guest",
            };

            var tags = "gruppo-utente";

            for (int i = 0; i <= aliass.Length - 1; i++) {
                var guid = Guid.NewGuid().ToString();
                var ugrp = ugrpRepo.Create(guid, aliass[i], tags);
                ugrpList.Add(ugrp);
            }

            var array = ugrpList.ToArray();
            return array;
        }

        public Anth_FunctionGroupModel[] FunctionGroups() {
            var fgrpList = new List<Anth_FunctionGroupModel>();

            var aliass = new string[] {
                "Master",
                "Admin",
                "Random",
                "Guest",
            };

            var tags = "gruppo-funzione";

            for (int i = 0; i <= aliass.Length - 1; i++) {
                var guid = Guid.NewGuid().ToString();
                var fgrp = funcRepo.Create(guid, aliass[i], tags);
                fgrpList.Add(fgrp);
            }

            var array = fgrpList.ToArray();
            return array;
        }
    }
}