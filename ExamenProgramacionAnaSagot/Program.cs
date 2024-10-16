﻿using System;

namespace ExamenFactura
{
    class Factura
    {
        public int numeroFactura;
        public DateTime fechaFactura;
        public string nombreCliente;
        public double subTotalFactura;
        public double ISVFactura;
        public double descuentoFactura;
        public double totalPagar;
        public Detalle[] detalles;

        public Factura(int numeroFactura, DateTime fechaFactura, string nombreCliente, int cantidadDetalles)
        {
            this.numeroFactura = numeroFactura;
            this.fechaFactura = fechaFactura;
            this.nombreCliente = nombreCliente;
            this.detalles = new Detalle[cantidadDetalles];
        }

        public void CalcularTotales()
        {
            subTotalFactura = 0;
            ISVFactura = 0;
            descuentoFactura = 0;

            foreach (var detalle in detalles)
            {
                subTotalFactura += detalle.subTotalDetalle;
                descuentoFactura += detalle.descuentoDetalle;
                ISVFactura += detalle.ISVDetalle;
            }

            totalPagar = subTotalFactura - descuentoFactura + ISVFactura;
        }
    }

    class Detalle
    {
        public int linea;
        public string producto;
        public int cantidad;
        public double precio;
        public double descuentoDetalle;
        public double ISVDetalle;
        public double subTotalDetalle;

        public Detalle(int linea, string producto, int cantidad, double precio, double descuento, double isv)
        {
            this.linea = linea;
            this.producto = producto;
            this.cantidad = cantidad;
            this.precio = precio;
            this.descuentoDetalle = descuento;
            this.ISVDetalle = isv;
            this.subTotalDetalle = CalcularSubTotal();
        }

        private double CalcularSubTotal()
        {
            return cantidad * precio - (cantidad * precio * descuentoDetalle) + (cantidad * precio * ISVDetalle);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Factura {i + 1}:");
                Console.Write("Número de factura: ");
                int numeroFactura = int.Parse(Console.ReadLine());
                Console.Write("Fecha de factura (yyyy-mm-dd): ");
                DateTime fechaFactura = DateTime.Parse(Console.ReadLine());
                Console.Write("Nombre del cliente: ");
                string nombreCliente = Console.ReadLine();

                Factura factura = new Factura(numeroFactura, fechaFactura, nombreCliente, 3);

                for (int j = 0; j < 3; j++)
                {
                    Console.WriteLine($"Detalle {j + 1}:");
                    Console.Write("Producto: ");
                    string producto = Console.ReadLine();
                    Console.Write("Cantidad: ");
                    int cantidad = int.Parse(Console.ReadLine());
                    Console.Write("Precio: ");
                    double precio = double.Parse(Console.ReadLine());
                    Console.Write("Descuento (ej. 0.10 para 10%): ");
                    double descuento = double.Parse(Console.ReadLine());
                    Console.Write("ISV (ej. 0.15 para 15%): ");
                    double isv = double.Parse(Console.ReadLine());

                    Detalle detalle = new Detalle(j + 1, producto, cantidad, precio, descuento, isv);
                    factura.detalles[j] = detalle;
                }

                factura.CalcularTotales();
                ImprimirFactura(factura);
            }
        }

        static void ImprimirFactura(Factura factura)
        {
            Console.WriteLine("\n--- Factura ---");
            Console.WriteLine($"Número: {factura.numeroFactura}");
            Console.WriteLine($"Fecha: {factura.fechaFactura.ToShortDateString()}");
            Console.WriteLine($"Cliente: {factura.nombreCliente}");
            Console.WriteLine($"Subtotal: {factura.subTotalFactura}");
            Console.WriteLine($"Descuento: {factura.descuentoFactura}");
            Console.WriteLine($"ISV: {factura.ISVFactura}");
            Console.WriteLine($"Total a Pagar: {factura.totalPagar}");
            Console.WriteLine("----------------\n");
        }
    }
}