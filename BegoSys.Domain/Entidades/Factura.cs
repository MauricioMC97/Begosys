using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    //Tabla que contiene las facturas de las ventas del día
    [Table("JBFACTURAS")]
    public partial class Factura
    {
        //Identificador de las facturas para ser usado por Entityframework
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDREGISTRO")]
        public long IdRegistro { get; set; }

        //Consecutivo que se reinicia al inicio del día
        [Column("IDPEDIDODIA")]
        public long IdPedidoDia { get; set; }

        //Resolución que autoriza la numeración de facturación
        [Column("NRORESOLUCIONDIAN")]
        public string NroResolucionDian { get; set; }

        //Número de la factura dado por la Dian
        [Column("NROFACTURADIAN")]
        public string NroFacturaDian { get; set; }

        //Fecha de la factura
        [Column("FECHA")]
        public DateTime Fecha { get; set; }

        //Dice si el pedido es para comer acá o para llevar
        [Column("IDTIPODESPACHO")]
        public long IdTipoDespacho { get; set; }

        //Valor del impuesto al consumo total de la factura
        [Column("IMPUESTO")]
        public double Impuesto { get; set; }

        //Valor total de la factura
        [Column("VALORTOTAL")]
        public double ValorTotal { get; set; }

        //Estado de la factura
        [Column("ESTADOFACTURA")]
        public string EstadoFactura { get; set; }

        //Identificador de la persona que registra la factura
        [Column("IDPERSONA")]
        public long IdPersona { get; set; }

        //Identificador del local donde se realizó la factura
        [Column("IDLOCAL")]
        public long IdLocal { get; set; }

        //Dirección a la cual se va a enviar el domicilio
        [Column("DIRECCION")]
        public string Direccion { get; set; }

        [Column("DOCCLIENTE")]
        public string DocCliente { get; set; }

        //Nombre del cliente que pide el domicilio
        [Column("CLIENTE")]
        public string Cliente { get; set; }

        //Teléfono del lugar donde se pide el domicilio
        [Column("TELEFONO")]
        public string Telefono { get; set; }
    }
}
