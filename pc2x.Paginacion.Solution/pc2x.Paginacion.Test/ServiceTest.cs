using Microsoft.VisualStudio.TestTools.UnitTesting;
using pc2x.Paginacion.Repository;
using pc2x.Paginacion.Service;

namespace pc2x.Paginacion.Test
{
    [TestClass]
    public class ServiceTest
    {
        [TestMethod]
        public void SERV_FacturaServicio_GetAll_NotNull()
        {
            var s = new FacturaService(new FacturaRepository());
            var x = s.GetAll(50, 0);

            Assert.IsNotNull(x);
        }
    }
}
