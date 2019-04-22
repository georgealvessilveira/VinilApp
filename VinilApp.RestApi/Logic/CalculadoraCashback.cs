using System.Collections.Generic;
using NodaTime;
using VinilApp.RestApi.Model;
using VinilApp.RestApi.Util;

namespace VinilApp.RestApi.Logic
{
    public class CalculadoraCashback
    {
        private readonly IsoDayOfWeek _dayOfWeek ;

        public CalculadoraCashback()
        {
            _dayOfWeek = DateUtil.Now().DayOfWeek;
        }

        public void Calcula(Venda venda)
        {
            venda.ItensVenda.ForEach(itemVenda =>
            {
                switch (_dayOfWeek)
                {
                    case IsoDayOfWeek.Sunday:
                        new Domingo().CalculaCashback(itemVenda);
                        break;
                    case IsoDayOfWeek.Monday:
                        new Segunda().CalculaCashback(itemVenda);
                        break;
                    case IsoDayOfWeek.Tuesday:
                        new Terca().CalculaCashback(itemVenda);
                        break;
                    case IsoDayOfWeek.Wednesday:
                        new Quarta().CalculaCashback(itemVenda);
                        break;
                    case IsoDayOfWeek.Thursday:
                        new Quinta().CalculaCashback(itemVenda);
                        break;
                    case IsoDayOfWeek.Friday:
                        new Sexta().CalculaCashback(itemVenda);
                        break;
                    case IsoDayOfWeek.Saturday:
                        new Sabado().CalculaCashback(itemVenda);
                        break;
                }
            });
        }
    }

    abstract class DiaSemana
    {
        public abstract void CalculaCashback(ItemVenda itemVenda);
    }

    class Domingo : DiaSemana
    {
        public override void CalculaCashback(ItemVenda itemVenda)
        {
            switch (itemVenda.Disco.Genero)
            {
                case "Pop":
                    itemVenda.Cashback = 0.25;
                    break;
                case "Mpb":
                    itemVenda.Cashback = 0.3;
                    break;
                case "Classic":
                    itemVenda.Cashback = 0.35;
                    break;
                case "Rock":
                    itemVenda.Cashback = 0.4;
                    break;
            }
        }
    }

    class Segunda : DiaSemana
    {
        public override void CalculaCashback(ItemVenda itemVenda)
        {
            switch (itemVenda.Disco.Genero)
            {
                case "Pop":
                    itemVenda.Cashback = 0.07;
                    break;
                case "Mpb":
                    itemVenda.Cashback = 0.05;
                    break;
                case "Classic":
                    itemVenda.Cashback = 0.03;
                    break;
                case "Rock":
                    itemVenda.Cashback = 0.10;
                    break;
            }
        }
    }

    class Terca : DiaSemana
    {
        public override void CalculaCashback(ItemVenda itemVenda)
        {
            switch (itemVenda.Disco.Genero)
            {
                case "Pop":
                    itemVenda.Cashback = 0.06;
                    break;
                case "Mpb":
                    itemVenda.Cashback = 0.10;
                    break;
                case "Classic":
                    itemVenda.Cashback = 0.05;
                    break;
                case "Rock":
                    itemVenda.Cashback = 0.15;
                    break;
            }
        }
    }

    class Quarta : DiaSemana
    {
        public override void CalculaCashback(ItemVenda itemVenda)
        {
            switch (itemVenda.Disco.Genero)
            {
                case "Pop":
                    itemVenda.Cashback = 0.02;
                    break;
                case "Mpb":
                    itemVenda.Cashback = 0.15;
                    break;
                case "Classic":
                    itemVenda.Cashback = 0.08;
                    break;
                case "Rock":
                    itemVenda.Cashback = 0.15;
                    break;
            }
        }
    }

    class Quinta : DiaSemana
    {
        public override void CalculaCashback(ItemVenda itemVenda)
        {
            switch (itemVenda.Disco.Genero)
            {
                case "Pop":
                    itemVenda.Cashback = 0.10;
                    break;
                case "Mpb":
                    itemVenda.Cashback = 0.20;
                    break;
                case "Classic":
                    itemVenda.Cashback = 0.13;
                    break;
                case "Rock":
                    itemVenda.Cashback = 0.15;
                    break;
            }
        }
    }

    class Sexta : DiaSemana
    {
        public override void CalculaCashback(ItemVenda itemVenda)
        {
            switch (itemVenda.Disco.Genero)
            {
                case "Pop":
                    itemVenda.Cashback = 0.15;
                    break;
                case "Mpb":
                    itemVenda.Cashback = 0.25;
                    break;
                case "Classic":
                    itemVenda.Cashback = 0.18;
                    break;
                case "Rock":
                    itemVenda.Cashback = 0.20;
                    break;
            }
        }
    }

    class Sabado : DiaSemana
    {
        public override void CalculaCashback(ItemVenda itemVenda)
        {
            switch (itemVenda.Disco.Genero)
            {
                case "Pop":
                    itemVenda.Cashback = 0.20;
                    break;
                case "Mpb":
                    itemVenda.Cashback = 0.30;
                    break;
                case "Classic":
                    itemVenda.Cashback = 0.25;
                    break;
                case "Rock":
                    itemVenda.Cashback = 0.40;
                    break;
            }
        }
    }
}