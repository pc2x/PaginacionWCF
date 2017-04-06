using pc2x.Paginacion.WCF.Core.Contracts;
using pc2x.Paginacion.Web.ViewModels;
using System;
using System.Linq;
using System.ServiceModel;
using System.Web.Mvc;

namespace pc2x.Paginacion.Web.Controllers
{
    public class FacturacionController : Controller
    {
        private readonly ChannelFactory<IFacturacionService> _channel;
        private readonly IFacturacionService _proxy;
        public FacturacionController()
        {
            _channel = new ChannelFactory<IFacturacionService>("FacturacionServiceEndPoint");
            _proxy = _channel.CreateChannel();
        }
        public ActionResult Index(int pageSize = 10, int page = 1)
        {
            try
            {
                var r = _proxy.ObtenerFacturas(pageSize, page);

                //can be used automapper or similar
                var model = new FacturaListVm
                {
                    Paginacion = new PaginacionInfoVm
                    {
                        PageSize = r.Paginacion.PageSize,
                        CurrrentPage = r.Paginacion.CurrrentPage,
                        TotalPages = r.Paginacion.TotalPages,
                        TotalRecords = r.Paginacion.TotalRecords
                    },

                    Facturas = r.Facturas.Select(m => new FacturaVm
                    {
                        Folio = m.Folio,
                        FechaExpedicion = m.FechaExpedicion,
                        LugarExpedicion = m.LugarExpedicion,
                        RfcEmisor = m.RfcEmisor,
                        RfcReceptor = m.RfcReceptor
                    })
                };

                return View(model);
            }
            catch (FaultException e)
            {
                ViewBag.Error = e.Message;
                return View(new FacturaListVm());
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error inesperado, por favor intenta más tarde.";
                return View(new FacturaListVm());
            }
        }

        //public ActionResult Detalle(string folio)
        //{

        //}


    }
}