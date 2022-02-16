using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public static class DateUltil
{

    public static string ToDateFull(this string Date)
    {
        var meses = new string[] {"Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
        var dateArr =  Date.Split("/");
        var mes = Int32.Parse(dateArr[1]);
        var dateFull = $"{dateArr[0]} de {meses[mes-1]} de {dateArr[2]}";
        return dateFull;
   
    }

    public static string ToDateMin(this string Date)
    {
        var meses = new string[] { "Jan", "Fev", "Mar", "Abr", "Maio", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez" };             
        var dateArr = Date.Split("/");
        var mes = Int32.Parse(dateArr[1]);
        var dateFull = $"{dateArr[0]} de {meses[mes - 1]} de {dateArr[2]}";
        return dateFull;
   
    }
}
