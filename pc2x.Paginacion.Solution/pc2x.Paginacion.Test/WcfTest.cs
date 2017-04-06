using Microsoft.VisualStudio.TestTools.UnitTesting;
using pc2x.Paginacion.WCF.Core.Contracts;
using System.ServiceModel;

namespace pc2x.Paginacion.Test
{
    [TestClass]
    public class WcfTest
    {
        private ChannelFactory<IFacturacionService> GetFactory()
        {
            return new ChannelFactory<IFacturacionService>(new WSHttpBinding(), "http://localhost:62170/FacturacionService.svc");
        }

        [TestMethod]
        public void WCF_FacturacionService_ObtenerFacturasGetAll_NotNull()
        {
            var f = GetFactory();
            var proxy = f.CreateChannel();

            var x = proxy.ObtenerFacturas(20, 1);

            f.Close();
            Assert.IsNotNull(x);

        }

        [TestMethod]
        public void WCF_FacturacionService_ObtenerDetalleFactura_NotNull()
        {
            var f = GetFactory();
            var proxy = f.CreateChannel();

            var x = proxy.ObtenerDetalleFactura("F000000001");

            f.Close();
            Assert.IsNotNull(x);

        }
    }
}
