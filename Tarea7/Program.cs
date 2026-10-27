using System;
using System.Collections.Generic;
using System.IO;

// ===================== CLASES =====================

class CuentaBancaria
{
    private string titular;
    private int saldo;

    public CuentaBancaria(string titular, int saldo)
    {
        this.titular = titular;
        this.saldo = saldo;
    }

    // Propiedad de solo lectura: tiene get pero no set
    public int Saldo
    {
        get { return saldo; }
    }

    public void Depositar(int monto)
    {
        if (monto <= 0)
        {
            Console.WriteLine("El deposito debe ser mayor a 0");
            return;
        }
        saldo += monto;
        Console.WriteLine("Deposito de " + monto + " Bs. Saldo: " + saldo + " Bs");
    }

    public void Retirar(int monto)
    {
        if (monto <= 0)
        {
            Console.WriteLine("El retiro debe ser mayor a 0");
            return;
        }
        if (monto > saldo)
        {
            Console.WriteLine("Saldo insuficiente. Saldo: " + saldo + " Bs");
            return;
        }
        saldo -= monto;
        Console.WriteLine("Retiro de " + monto + " Bs. Saldo: " + saldo + " Bs");
    }

    public void MostrarResumen()
    {
        Console.WriteLine("Cuenta de " + titular + " \u2502 Saldo final: " + saldo + " Bs");
    }
}

class Libro
{
    private string titulo;
    private bool disponible;

    public Libro(string titulo)
    {
        this.titulo = titulo;
        this.disponible = true;   // al crearse, siempre esta disponible
    }

    public string Titulo
    {
        get { return titulo; }
    }

    public bool Disponible
    {
        get { return disponible; }
    }

    // Devuelve false si el libro ya estaba prestado
    public bool Prestar()
    {
        if (!disponible)
        {
            return false;
        }
        disponible = false;
        return true;
    }
}

class Contacto
{
    private string nombre;
    private string telefono;

    public Contacto(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
    }

    public string Nombre
    {
        get { return nombre; }
    }

    public string Telefono
    {
        get { return telefono; }
    }

    public string ALinea()
    {
        return nombre + ";" + telefono;
    }
}

// Mi propia clase: Plato (comida tipica de mi ciudad, Cochabamba)
class Plato
{
    private string nombre;
    private int precio;
    private int porciones;

    public Plato(string nombre, int precio, int porciones)
    {
        this.nombre = nombre;
        Precio = precio;            // usa la propiedad para validar
        this.porciones = porciones;
    }

    public string Nombre
    {
        get { return nombre; }
    }

    // Propiedad con validacion: el precio no puede ser negativo
    public int Precio
    {
        get { return precio; }
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException("precio");
            precio = value;
        }
    }

    // Metodo 1: sirve porciones y descuenta del total disponible
    public void Servir(int cantidad)
    {
        if (cantidad <= 0)
        {
            Console.WriteLine("La cantidad debe ser mayor a 0");
            return;
        }
        if (cantidad > porciones)
        {
            Console.WriteLine("No hay porciones suficientes de " + nombre);
            return;
        }
        porciones -= cantidad;
        Console.WriteLine("Servido: " + cantidad + " " + nombre + ". Total: " + (cantidad * precio) + " Bs. Quedan " + porciones);
    }

    // Metodo 2: convierte el plato a una linea para el archivo
    public string ALinea()
    {
        return nombre + ";" + precio + ";" + porciones;
    }
}

// ===================== PROGRAMA =====================

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        int opcion;
        do
        {
            Console.WriteLine();
            Console.WriteLine("===== TAREA 7: CLASES Y ARCHIVOS =====");
            Console.WriteLine("Estudiante: Tu Nombre Completo");
            Console.WriteLine("1. Cuenta bancaria");
            Console.WriteLine("2. Prestamo de libros");
            Console.WriteLine("3. Agenda de contactos con archivo");
            Console.WriteLine("4. Mi propia clase");
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
                case 4: Ejercicio4(); break;
                case 0: Console.WriteLine("Hasta luego"); break;
                default: Console.WriteLine("Opcion no valida"); break;
            }
        } while (opcion != 0);
    }

    // Lee un entero; si escribe algo que no es numero, vuelve a pedirlo
    static int LeerEntero(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Debes escribir un numero entero");
            }
        }
    }

    // Ejercicio 1: Cuenta bancaria
    static void Ejercicio1()
    {
        Console.Write("Titular: ");
        string titular = Console.ReadLine();
        int saldoInicial = LeerEntero("Saldo inicial: ");
        CuentaBancaria cuenta = new CuentaBancaria(titular, saldoInicial);

        int deposito = LeerEntero("Monto a depositar: ");
        cuenta.Depositar(deposito);

        int retiro = LeerEntero("Monto a retirar: ");
        cuenta.Retirar(retiro);

        cuenta.MostrarResumen();
    }

    // Ejercicio 2: Prestamo de libros
    static void Ejercicio2()
    {
        List<Libro> libros = new List<Libro>();
        libros.Add(new Libro("El Principito"));
        libros.Add(new Libro("Don Quijote"));
        libros.Add(new Libro("Rayuela"));

        for (int i = 0; i < 2; i++)
        {
            Console.Write("Titulo del libro a prestar: ");
            string buscado = Console.ReadLine();

            Libro encontrado = null;
            foreach (Libro l in libros)
            {
                if (l.Titulo.ToLower() == buscado.ToLower())
                {
                    encontrado = l;
                }
            }

            if (encontrado == null)
            {
                Console.WriteLine("No existe el libro " + buscado);
            }
            else if (encontrado.Prestar())
            {
                Console.WriteLine("Prestado: " + encontrado.Titulo);
            }
            else
            {
                Console.WriteLine(encontrado.Titulo + " ya esta prestado");
            }
        }

        Console.WriteLine("Estado de la biblioteca:");
        foreach (Libro l in libros)
        {
            string estado = l.Disponible ? "Disponible" : "Prestado";
            Console.WriteLine(l.Titulo + " - " + estado);
        }
    }

    // Ejercicio 3: Agenda de contactos con archivo
    static void Ejercicio3()
    {
        int cantidad = LeerEntero("Cantidad de contactos: ");
        List<Contacto> contactos = new List<Contacto>();

        for (int i = 0; i < cantidad; i++)
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Telefono: ");
            string telefono = Console.ReadLine();
            contactos.Add(new Contacto(nombre, telefono));
        }

        List<string> lineas = new List<string>();
        foreach (Contacto c in contactos)
        {
            lineas.Add(c.ALinea());
        }
        File.WriteAllLines("contactos.txt", lineas);
        Console.WriteLine("Contactos guardados: " + contactos.Count);

        Console.WriteLine("Leyendo contactos.txt:");
        foreach (string linea in File.ReadAllLines("contactos.txt"))
        {
            string[] partes = linea.Split(';');
            Console.WriteLine(partes[0] + " - Tel: " + partes[1]);
        }
    }

    // Ejercicio 4: Mi propia clase (Plato)
    static void Ejercicio4()
    {
        int cantidad = LeerEntero("Cantidad de platos: ");
        List<Plato> platos = new List<Plato>();

        for (int i = 0; i < cantidad; i++)
        {
            Console.Write("Nombre del plato: ");
            string nombre = Console.ReadLine();
            int precio = LeerEntero("Precio (Bs): ");
            int porciones = LeerEntero("Porciones disponibles: ");

            try
            {
                platos.Add(new Plato(nombre, precio, porciones));
            }
            catch (ArgumentOutOfRangeException)   // el set de Precio lanzo el error
            {
                Console.WriteLine("Error: el precio no puede ser negativo. Plato no registrado");
            }
        }

        // Probamos el metodo Servir con el primer plato (si existe)
        if (platos.Count > 0)
        {
            int pedido = LeerEntero("Porciones a servir del primer plato: ");
            platos[0].Servir(pedido);
        }

        List<string> lineas = new List<string>();
        foreach (Plato p in platos)
        {
            lineas.Add(p.ALinea());
        }
        File.WriteAllLines("platos.txt", lineas);
        Console.WriteLine("Platos guardados: " + platos.Count);

        Console.WriteLine("Leyendo platos.txt:");
        foreach (string linea in File.ReadAllLines("platos.txt"))
        {
            string[] partes = linea.Split(';');
            Console.WriteLine(partes[0] + " - " + partes[1] + " Bs - Porciones: " + partes[2]);
        }
    }
}

