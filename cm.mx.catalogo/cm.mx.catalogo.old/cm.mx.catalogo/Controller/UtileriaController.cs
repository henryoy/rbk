using cm.mx.catalogo.Model;
using cm.mx.dbCore.Interfaces;
using Newtonsoft.Json.Linq;
using NHibernate;
using NHibernate.Criterion;
using PushSharp.Apple;
using PushSharp.Core;
using PushSharp.Google;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace cm.mx.catalogo.Controller
{
    public class UtileriaController : IController
    {
        #region propiedades
        private List<string> _errores = new List<string>();
        private List<string> _mensajes = new List<string>();
        private string _mensaje = string.Empty;
        private bool _exito = false;

        private int _totalColumnas = 0;
        public int TotalColumnas
        {
            get { return _totalColumnas; }
            set { _totalColumnas = value; }
        }
        public List<string> Errores
        {
            get { return _errores; }
        }
        public bool Exito
        {
            get
            {
                return _exito;
            }
            set
            {
                _exito = value;
            }
        }
        public string Mensaje
        {
            get
            {
                return _mensaje;
            }
            set
            {
                _mensaje = value;
            }
        }
        public List<string> Mensajes
        {
            get { return _mensajes; }
        }
        #endregion

        public string GenerarFolio(int SucursalId)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            string folio = "";
            try
            {
                var sfecha = String.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
                var sSuc = String.Format("{0:00000}", SucursalId);
                folio = sfecha + "-" + sSuc;
            }
            catch (Exception ex)
            {
                _exito = false;
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }

            return folio;
        }

        public List<ICriterion> CrearCondion(List<CondicionDistribucion> condiciones)
        {
            var union = condiciones.Where(a => !string.IsNullOrEmpty(a.Nexo)).Select(a => a.Nexo).FirstOrDefault();
            if (string.IsNullOrEmpty(union)) union = "Y";
            List<ICriterion> lsCond = new List<ICriterion>();
            try
            {
                var lsiCriteriones = condiciones.GroupBy(u => u.Campo).Select(grp => grp.ToList()).ToList();
                lsiCriteriones.ForEach(c =>
                {
                    Disjunction ExpresionOr = null;
                    Conjunction ExpresionAnd = null;

                    c.ForEach(x =>
                    {
                        object val = null;
                        List<object> ls = new List<object>();
                        if (x.Campo.Equals("TarjetaID", StringComparison.OrdinalIgnoreCase)) x.Campo = "Membresiaid";
                        switch (x.Tipo)
                        {
                            case "Texto":
                                if (x.Valor.IndexOf(",") == -1) val = x.Valor;
                                else
                                    ls.AddRange(x.Valor.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList());
                                break;
                            case "Entero":
                                int _id;
                                if (x.Valor.IndexOf(",") == -1)
                                {
                                    int.TryParse(x.Valor, out _id);
                                    val = _id;
                                }
                                else
                                {
                                    var r = x.Valor.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                    r.ForEach(a =>
                                    {
                                        int.TryParse(a, out _id);
                                        ls.Add(_id);
                                    });
                                }
                                break;
                            case "Moneda":
                                double _cant;
                                if (x.Valor.IndexOf(",") == -1)
                                {
                                    double.TryParse(x.Valor, out _cant);
                                    val = _cant;
                                }
                                else
                                {
                                    var r = x.Valor.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                    r.ForEach(a =>
                                    {
                                        double.TryParse(a, out _cant);
                                        ls.Add(_cant);
                                    });
                                }
                                break;
                            case "Fecha":
                                DateTime _Fecha;
                                if (x.Valor.IndexOf(",") == -1)
                                {
                                    DateTime.TryParse(x.Valor, out _Fecha);
                                    val = _Fecha;
                                }
                                else
                                {
                                    var r = x.Valor.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                    r.ForEach(a =>
                                    {
                                        DateTime.TryParse(a, out _Fecha);
                                        ls.Add(_Fecha);
                                    });
                                }
                                break;
                            case "Decimal":
                                decimal _Dec;
                                if (x.Valor.IndexOf(",") == -1)
                                {
                                    decimal.TryParse(x.Valor, out _Dec);
                                    val = _Dec;
                                }
                                else
                                {
                                    var r = x.Valor.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                    r.ForEach(a =>
                                    {
                                        decimal.TryParse(a, out _Dec);
                                        ls.Add(_Dec);
                                    });
                                }
                                break;
                        }

                        union = string.IsNullOrEmpty(x.Nexo) ? union : x.Nexo;
                        if (union.Equals("O", StringComparison.OrdinalIgnoreCase))
                        {
                            switch (x.Operador)
                            {
                                case "=":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Eq(x.Campo, val));
                                    break;
                                case ">":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Gt(x.Campo, val));
                                    break;
                                case "<":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Lt(x.Campo, val));
                                    break;
                                case ">=":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Ge(x.Campo, val));
                                    break;
                                case "<=":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Le(x.Campo, val));
                                    break;
                                case "!=":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Eq(x.Campo, val));
                                    break;
                                case "In":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.In(x.Campo, ls));
                                    break;
                                case "Not in":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Not(Restrictions.In(x.Campo, ls)));
                                    break;
                                case "Like":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Like(Projections.Cast(NHibernateUtil.String, Projections.Property(x.Campo)), x.Valor, MatchMode.Anywhere));
                                    break;
                                case "En mes":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Eq(Projections.SqlFunction("month", NHibernateUtil.DateTime, Projections.Property(x.Campo)), val));
                                    break;
                            }
                        }
                        else
                        {
                            switch (x.Operador)
                            {
                                case "=":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Eq(x.Campo, val));
                                    break;
                                case ">":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Gt(x.Campo, val));
                                    break;
                                case "<":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Lt(x.Campo, val));
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Lt(x.Campo, val));
                                    break;
                                case ">=":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Ge(x.Campo, val));
                                    break;
                                case "<=":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Le(x.Campo, val));
                                    break;
                                case "!=":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Eq(x.Campo, val));
                                    break;
                                case "In":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.In(x.Campo, ls));
                                    break;
                                case "Not in":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Not(Restrictions.In(x.Campo, ls)));
                                    break;
                                case "Like":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Like(Projections.Cast(NHibernateUtil.String, Projections.Property(x.Campo)), x.Valor, MatchMode.Anywhere));
                                    break;
                                case "En mes":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Eq(Projections.SqlFunction("month", NHibernateUtil.DateTime, Projections.Property(x.Campo)), val));
                                    break;
                            }
                        }
                    });
                    if (ExpresionAnd != null) lsCond.Add(ExpresionAnd);
                    if (ExpresionOr != null) lsCond.Add(ExpresionOr);
                });
                _exito = true;
            }
            catch (Exception ex)
            {
                lsCond = new List<ICriterion>();
                _exito = false;
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }
            return lsCond;
        }

        private bool CrearTabla(List<string> columnas, GridView _grid)
        {
            _exito = false;
            try
            {
                CamposDistribucionRepository rCampos = new CamposDistribucionRepository();
                var lsColumnas = rCampos.GetCampos(columnas);
                _grid.Columns.Clear();
                lsColumnas.ForEach(x =>
                {
                    BoundField col = new BoundField();
                    col.HeaderText = x.Descripcion;
                    col.DataField = x.Campo;
                    if (!string.IsNullOrEmpty(x.Formato)) col.DataFormatString = "{0:" + x.Formato + "}";
                    _grid.Columns.Add(col);
                });
                _exito = true;
            }
            catch (Exception ex)
            {
                _exito = false;
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }
            return _exito;
        }

        public bool ProbarDistribucion(Distribucion obj, GridView grid)
        {
            _exito = false;
            _errores = new List<string>();
            try
            {
                List<string> campos = obj.Campos.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (CrearTabla(campos, grid))
                {
                    UsuarioRepository rUsuario = new UsuarioRepository();
                    var cond = CrearCondion(obj.Condiciones.ToList());
                    var ls = rUsuario.GetUsuarios(cond);
                    grid.DataSource = ls;
                    grid.DataBind();
                    grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                    _exito = true;
                }
            }
            catch (Exception ex)
            {
                _exito = false;
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }
            return _exito;
        }

        public void sendAppleNotification(Notificacion notificacion, UsuarioDispositivo usuario)
        {
            string file = ConfigurationManager.AppSettings["PathCertificado1"];//@"C:\privado\Push Produccion.p12"; //HttpContext.Current.Server.MapPath("~/privado/Push Produccion.p12");
            var config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Production, file, "mi#esel16");

            // Create a new broker
            var apnsBroker = new ApnsServiceBroker(config);

            // Wire up events
            apnsBroker.OnNotificationFailed += (notification, aggregateEx) =>
            {

                aggregateEx.Handle(ex =>
                {

                    // See what kind of exception it was to further diagnose
                    if (ex is ApnsNotificationException)
                    {
                        var notificationException = (ApnsNotificationException)ex;

                        // Deal with the failed notification
                        var apnsNotification = notificationException.Notification;
                        var statusCode = notificationException.ErrorStatusCode;

                        Console.WriteLine("Apple Notification Failed: ID={apnsNotification.Identifier}, Code={statusCode}");
                    }
                    else
                    {
                        // Inner exception might hold more useful information like an ApnsConnectionException			
                        Console.WriteLine("Apple Notification Failed for some unknown reason : {ex.InnerException}");
                    }

                    // Mark it as handled
                    return true;
                });
            };

            apnsBroker.OnNotificationSucceeded += (notification) =>
            {
                //Console.WriteLine("Apple Notification Sent!");
                CatalogoController cCatalogoTemp = new CatalogoController();
                Notificacion oNot = cCatalogoTemp.GetNotificacion(int.Parse(notification.Tag.ToString()));
                if (oNot != null)
                {
                    oNot.Enviado = true;
                    cCatalogoTemp.ActualizaNotificacion(oNot);
                }
            };

            // Start the broker
            apnsBroker.Start();


            apnsBroker.QueueNotification(new ApnsNotification
            {
                DeviceToken = usuario.Token,
                Payload = JObject.Parse("{\"aps\":{\"alert\":\"" + notificacion.Mensaje + "\",\"badge\":\"2\",\"sound\":\"default\"}}"),
                Tag = notificacion.NotificacionID
            });

            apnsBroker.Stop();

        }

        public void sendGoogleNotifications(Notificacion notificacion, UsuarioDispositivo usuario)
        {
            var config = new GcmConfiguration("122905653622", "AIzaSyCLhUuzR_Dqe0Vf4v3WKbvr31akgwc16_8", null);

            var gcmBroker = new GcmServiceBroker(config);


            // Wire up events
            gcmBroker.OnNotificationFailed += (notification, aggregateEx) =>
            {

                aggregateEx.Handle(ex =>
                {

                    // See what kind of exception it was to further diagnose
                    if (ex is GcmNotificationException)
                    {
                        var notificationException = (GcmNotificationException)ex;

                        // Deal with the failed notification
                        var gcmNotification = notificationException.Notification;
                        var description = notificationException.Description;

                        Console.WriteLine("GCM Notification Failed: ID={gcmNotification.MessageId}, Desc={description}");
                        //richTextBox1.Text += "\n" + $"GCM Notification Failed: ID={gcmNotification.MessageId}, Desc={description}";
                    }
                    else if (ex is GcmMulticastResultException)
                    {
                        var multicastException = (GcmMulticastResultException)ex;

                        foreach (var succeededNotification in multicastException.Succeeded)
                        {
                            Console.WriteLine("GCM Notification Failed: ID={succeededNotification.MessageId}");
                            //richTextBox1.Text += "\n" + $"GCM Notification Failed: ID={succeededNotification.MessageId}";
                        }

                        foreach (var failedKvp in multicastException.Failed)
                        {
                            var n = failedKvp.Key;
                            var e1 = failedKvp.Value;

                            Console.WriteLine("GCM Notification Failed: ID={n.MessageId}, Desc={e1.InnerException}");
                            //richTextBox1.Text += "\n" + $"GCM Notification Failed: ID={n.MessageId}, Desc={e1.InnerException}";
                        }

                    }
                    else if (ex is DeviceSubscriptionExpiredException)
                    {
                        var expiredException = (DeviceSubscriptionExpiredException)ex;

                        var oldId = expiredException.OldSubscriptionId;
                        var newId = expiredException.NewSubscriptionId;


                        Console.WriteLine("Device RegistrationId Expired: {oldId}");
                        //richTextBox1.Text += "\n" + $"Device RegistrationId Expired: {oldId}";
                        //ExecuteQuery(false, notification.RegistrationIds[0]);

                        if (!string.IsNullOrWhiteSpace(newId))
                        {
                            // If this value isn't null, our subscription changed and we should update our database
                            Console.WriteLine("Device RegistrationId Changed To: {newId}");
                            //richTextBox1.Text += "\n" + $"Device RegistrationId Changed To: {newId}";
                        }
                    }
                    else if (ex is RetryAfterException)
                    {
                        var retryException = (RetryAfterException)ex;
                        // If you get rate limited, you should stop sending messages until after the RetryAfterUtc date
                        Console.WriteLine("GCM Rate Limited, don't send more until after {retryException.RetryAfterUtc}");
                        //richTextBox1.Text += "\n" + $"GCM Rate Limited, don't send more until after {retryException.RetryAfterUtc}";
                    }
                    else
                    {
                        Console.WriteLine("GCM Notification Failed for some unknown reason");
                        //richTextBox1.Text += "\n" + "GCM Notification Failed for some unknown reason";
                    }

                    // Mark it as handled
                    return true;
                });
            };

            gcmBroker.OnNotificationSucceeded += (notification) =>
            {
                Console.WriteLine("GCM Notification Sent!");

                CatalogoController cCatalogoTemp = new CatalogoController();
                Notificacion oNot = cCatalogoTemp.GetNotificacion(int.Parse(notification.Tag.ToString()));
                if (oNot != null)
                {
                    oNot.Enviado = true;
                    cCatalogoTemp.ActualizaNotificacion(oNot);
                }
            };

            gcmBroker.Start();

            GcmNotification obj = new GcmNotification();
            obj.RegistrationIds = new List<string> { usuario.Token };
            obj.Data = JObject.Parse("{ \"notification\" : {\"body\" : \"" + notificacion.Mensaje + "\",\"icon\" : \"ic_launcher.png\",\"title\" : \"" + "Friday's App" + "\"} }");
            obj.Tag = notificacion.NotificacionID;
            gcmBroker.QueueNotification(obj);
            gcmBroker.Stop();

        }

    }
}
