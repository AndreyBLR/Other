using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.Interfaces.PresenterInterfaces.Handler;
using MBPresentation.Interfaces.ViewInterfaces.Handler;
using MBPresentation.ServiceReference;

namespace MBPresentation.Presenters.Handler
{
    public class ImageHttpHandlerPresenter:IImageHttpHandlerPresenter
    {
        private IImageHttpHandler _handler;
        public ImageHttpHandlerPresenter(IImageHttpHandler handler)
        {
            _handler = handler;
            _handler.ImageReading += ReadImage;
        }

        public void ReadImage()
        {
            var svc = new ModelServiceClient();
            _handler.Image = svc.ReadImageById(_handler.ImageId);
        }
    }
}
