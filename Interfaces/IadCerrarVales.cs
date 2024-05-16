﻿using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadCerrarVales
    {
        public Task<IEnumerable<adGetCerrarValesModel>> getInformationVale(int CompanyID, string placa);

        public Task<IEnumerable<mensaje>> CerrarVale(string placa, int userID, int CompanyID, double CantidadGalones, double TotalDolares, int Odometro);

        public Task<IEnumerable<rpValesCerradosModel>> getValesCerrados(int UserID,int CompanyID, string FechaIncio, string fechaFin);

        public Task<IEnumerable<rpValesCerradosXPlacaModel>> getValesCerradosXPlaca(int UserID, int CompanyID,string Placa, string FechaIncio, string fechaFin);
    }
}
