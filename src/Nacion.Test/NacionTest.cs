using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Nacion.Core;
using Nacion.DataLayer;

namespace Nacion.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class NacionTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestBasicoCredito()
        {
            DataTable dt = Credito.Instancia.GetCuotas();
            Assert.AreEqual(dt.Rows.Count, 240, "La cantidad de cuotas debe ser de 240");
        }

        [TestMethod]
        public void TestSiguienteCuota()
        {
            Cuota cuota = Credito.Instancia.GetSiguienteCuota();
            Assert.AreNotEqual(cuota, null, "La siguiente cuota no puede ser nula.");
        }

        [TestMethod]
        public void TestRestoAPagar()
        {
            decimal resto = Credito.Instancia.GetRestoAPagar();
            bool restoValido = resto > 0;
            Assert.AreEqual(restoValido, true, "El resto a pagar debe ser mayor a cero.");
        }

        [TestMethod]
        public void TestVencimientoActual()
        {
            DateTime vencimiento = Credito.Instancia.GetUltimoVencimientoActual();
            Assert.AreNotEqual(vencimiento, DateTime.MinValue, "Vencimiento actual inválido.");
        }

        [TestMethod]
        public void TestTotalPagadoALaFecha()
        {
            decimal total = Credito.Instancia.GetTotalPagado();
            Assert.AreNotEqual(total, 0, "Total pagado inválido.");
        }

        [TestMethod]
        public void TestCantidadCuotasPagas()
        {
            int total = Credito.Instancia.GetCantidadCuotasPagas();
            Assert.AreEqual(total, 32, "Cantidad de cuotas pagas inválido.");
        }

        [TestMethod]
        public void TestCantidadCuotasNuevas()
        {
            int total = Credito.Instancia.GetCantidadCuotasNuevas();
            Assert.AreEqual(total, 208, "Cantidad de cuotas nuevas inválido.");
        }

        [TestMethod]
        public void TestPrimerVencimiento()
        {
            DateTime vencimiento = Credito.Instancia.GetPrimerVencimientoOriginal();
            Assert.AreNotEqual(vencimiento, DateTime.MinValue, "Primer vencimiento original inválido.");
        }

        [TestMethod]
        public void TestUltimoVencimiento()
        {
            DateTime vencimiento = Credito.Instancia.GetUltimoVencimientoOriginal();
            Assert.AreNotEqual(vencimiento, DateTime.MinValue, "Ultimo vencimiento original inválido.");
        }

        [TestMethod]
        public void TestInfoGeneral()
        {
            InfoGeneral info = Credito.Instancia.GetInfoGeneral();
            Assert.AreEqual(info.Cbu, "01100181 - 30001809148651", "Info general inválida.");
        }

        [TestMethod]
        public void TestGetCuotaEspecifica()
        {
            Cuota cuota = Credito.Instancia.GetCuotaNro(15);
            Assert.AreEqual(cuota.Nro, 15, "El nro de cuota debería ser el 15.");
        }

        [TestMethod]
        public void TestPagarCuota()
        {
            Cuota siguienteCuota = Credito.Instancia.GetSiguienteCuota();

            Credito.Instancia.PagarCuotaNro(siguienteCuota.Nro + 10);
            Cuota cuota = Credito.Instancia.GetCuotaNro(siguienteCuota.Nro + 10);
            Assert.AreEqual(cuota.Status, StatusCuota.Nueva, "Solo la siguiente cuota disponible podría pagarse.");

            Credito.Instancia.PagarCuotaNro(siguienteCuota.Nro);
            cuota = Credito.Instancia.GetCuotaNro(siguienteCuota.Nro);
            Assert.AreEqual(cuota.Status, StatusCuota.Pagada, "La siguiente cuota disponible debería estar paga ahora.");

            Credito.Instancia.AdelantarCuotaNro(siguienteCuota.Nro);
            cuota = Credito.Instancia.GetCuotaNro(siguienteCuota.Nro);
            Assert.AreNotEqual(cuota.Status, StatusCuota.Adelantada, "No se puede adelantar una cuota paga.");

            Credito.Instancia.ResetearCuotaNro(siguienteCuota.Nro);
            cuota = Credito.Instancia.GetCuotaNro(siguienteCuota.Nro);
            Assert.AreEqual(cuota.Status, StatusCuota.Nueva, "La siguiente cuota debería estar impaga ahora.");
        }

        [TestMethod]
        public void TestAdelantarCuota()
        {
            Cuota siguienteCuota = Credito.Instancia.GetSiguienteCuota();

            Credito.Instancia.AdelantarCuotaNro(siguienteCuota.Nro + 10);
            Cuota cuota = Credito.Instancia.GetCuotaNro(siguienteCuota.Nro + 10);
            Assert.AreEqual(cuota.Status, StatusCuota.Nueva, "Solo la siguiente cuota disponible puede adelantarse.");

            Credito.Instancia.AdelantarCuotaNro(siguienteCuota.Nro);
            cuota = Credito.Instancia.GetCuotaNro(siguienteCuota.Nro);
            Assert.AreEqual(cuota.Status, StatusCuota.Adelantada, "La siguiente cuota debería estar adelantada ahora.");

            Credito.Instancia.PagarCuotaNro(siguienteCuota.Nro);
            cuota = Credito.Instancia.GetCuotaNro(siguienteCuota.Nro);
            Assert.AreNotEqual(cuota.Status, StatusCuota.Pagada, "No se puede pagar una cuota adelantada.");

            Credito.Instancia.ResetearCuotaNro(siguienteCuota.Nro);
            cuota = Credito.Instancia.GetCuotaNro(siguienteCuota.Nro);
            Assert.AreEqual(cuota.Status, StatusCuota.Nueva, "La siguiente cuota debería estar impaga ahora.");
        }

        [TestMethod]
        public void TestSimular()
        {
            ResultadoSimulacion rs = Credito.Instancia.Simular(2000);
            Assert.AreEqual(rs.NroCuotasAdelantadas, "7", "El número de cuotas adelantadas es inválido para ese monto.");
            Assert.AreEqual(rs.CapitalAdelantado, "819.96", "El capital adelantado es inválido para ese monto.");
            Assert.AreEqual(rs.InteresesAdelantados, "5923.33", "Los intereses adelantados son inválidos para ese monto.");
            Assert.AreEqual(rs.VencimientoActual, "6/10/2025", "el vencimiento actual es inválido para ese monto.");
            Assert.AreEqual(rs.NroSiguienteCuota, "41", "El siguiente nro de cuota es inválido para ese monto.");
            Assert.AreEqual(rs.DineroRestante, "92.83", "El dinero restante es inválido para ese monto.");
        }

        [TestMethod]
        public void TestVencimientoSiguienteCuota()
        {
            DateTime vencimiento = Credito.Instancia.GetVencimientoSiguienteCuota();
            Assert.AreNotEqual(vencimiento, DateTime.MinValue, "Vencimiento de la siguiente cuota inválido.");
        }

        [TestMethod]
        public void TestCantidadCuotasAdelantadas()
        {
            int total = Credito.Instancia.GetCantidadCuotasAdelantadas();
            Assert.AreEqual(total, 27, "Cantidad de cuotas adelantadas inválido.");
        }
    }
}
