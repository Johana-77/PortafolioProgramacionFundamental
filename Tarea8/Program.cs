using System;
using System.Collections.Generic;

// ===================== EJERCICIO 1: PEAJE DE VEHICULOS =====================
class Vehiculo
{
    protected string placa;

    public Vehiculo(string placa)
    {
        this.placa = placa;
    }

    public virtual string Tipo()
    {
        return "Vehiculo";
    }

    public virtual int CalcularPeaje()
    {
        return 10;
    }

    public void Mostrar()
    {
        Console.WriteLine(Tipo() + " " + placa + ": " + CalcularPeaje() + " Bs");
    }
}

class Auto : Vehiculo
{
    public Auto(string placa) : base(placa) { }

    public override string Tipo()
    {
        return "Auto";
    }
    // CalcularPeaje se hereda: paga 10 Bs
}

class Moto : Vehiculo
{
    public Moto(string placa) : base(placa) { }

    public override string Tipo()
    {
        return "Moto";
    }

    public override int CalcularPeaje()
    {
        return 5;
    }
}

class Camion : Vehiculo
{
    private int ejes;

    public Camion(string placa, int ejes) : base(placa)
    {
        this.ejes = ejes;
    }

    public override string Tipo()
    {
        return "Camion";
    }

    public override int CalcularPeaje()
    {
        return 10 + ejes * 8;
    }
}
// ===================== EJERCICIO 2: CUENTAS =====================
class Cuenta
{
    protected string titular;
    protected int saldo;

    public Cuenta(string titular, int saldo)
    {
        this.titular = titular;
        this.saldo = saldo;
    }

    public virtual bool Retirar(int monto)
    {
        if (saldo >= monto)
        {
            saldo = saldo - monto;
            return true;
        }
        return false;
    }

    public void IntentarRetiro(int monto)
    {
        bool aprobado = Retirar(monto);
        string estado = aprobado ? "aprobado" : "rechazado";
        Console.WriteLine(titular + ": retiro de " + monto + " Bs " + estado + ". Saldo: " + saldo + " Bs");
    }
}

class CuentaCorriente : Cuenta
{
    private int limiteSobregiro = 500;

    public CuentaCorriente(string titular, int saldo) : base(titular, saldo) { }

    public override bool Retirar(int monto)
    {
        if (saldo - monto < -limiteSobregiro)
        {
            return false;
        }
        saldo = saldo - monto;
        return true;
    }
}

class Program
{
    static void Main()
    {
        int opcion;
        do
        {
            Console.WriteLine();
            Console.WriteLine("===== TAREA 8: HERENCIA Y POLIMORFISMO =====");
            Console.WriteLine("Estudiante: Johana Candida Granado Huarcaya");
            Console.WriteLine("1. Peaje de vehiculos");
            Console.WriteLine("2. Cuenta de ahorro y corriente");
            Console.WriteLine("3. Mi propia jerarquia");
            Console.WriteLine("0. Salir");
            Console.Write("Elige una opcion: ");
            try
            {
                opcion = int.Parse(Console.ReadLine());
            }
            catch (Exception)   // si escribe letras, la opcion no vale
            {
                opcion = -1;
            }

            switch (opcion)
            {
                case 1: Ejercicio1(); break;
                case 2: Ejercicio2(); break;
                case 3: Ejercicio3(); break;
                case 0: Console.WriteLine("Hasta luego"); break;
                default: Console.WriteLine("Opcion no valida"); break;
            }
        } while (opcion != 0);
    }

    // Ejercicio 1: Peaje de vehiculos
    static void Ejercicio1()
    {
        List<Vehiculo> vehiculos = new List<Vehiculo>();

        Console.Write("Cuantos vehiculos pasaron: ");
        int n = int.Parse(Console.ReadLine());

        for (int i = 1; i <= n; i++)
        {
            Console.Write("Vehiculo " + i + " - tipo (1 Auto, 2 Moto, 3 Camion): ");
            int tipo = int.Parse(Console.ReadLine());
            Console.Write("Placa: ");
            string placa = Console.ReadLine();

            if (tipo == 1)
            {
                vehiculos.Add(new Auto(placa));
            }
            else if (tipo == 2)
            {
                vehiculos.Add(new Moto(placa));
            }
            else if (tipo == 3)
            {
                Console.Write("Ejes: ");
                int ejes = int.Parse(Console.ReadLine());
                vehiculos.Add(new Camion(placa, ejes));
            }
            else
            {
                Console.WriteLine("Tipo no valido, se omite este vehiculo");
            }
        }

        int total = 0;
        foreach (Vehiculo v in vehiculos)
        {
            v.Mostrar();
            total = total + v.CalcularPeaje();
        }
        Console.WriteLine("Total recaudado: " + total + " Bs");
    }

    // Ejercicio 2: Cuenta de ahorro y corriente
    static void Ejercicio2()
    {
        Console.Write("Saldo inicial: ");
        int saldoInicial = int.Parse(Console.ReadLine());
        Console.Write("Monto a retirar: ");
        int monto = int.Parse(Console.ReadLine());

        Cuenta ahorro = new Cuenta("Ahorro", saldoInicial);
        Cuenta corriente = new CuentaCorriente("Corriente", saldoInicial);

        ahorro.IntentarRetiro(monto);
        corriente.IntentarRetiro(monto);
    }

    // Ejercicio 3: Mi propia jerarquia
    static void Ejercicio3()
    {
        // escribe aqui tu codigo
    }
}