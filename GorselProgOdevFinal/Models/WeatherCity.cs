namespace GorselProgOdevFinal.Models
{
    public class WeatherCity
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public HtmlWebViewSource WidgetSource
        {
            get
            {
                var source = new HtmlWebViewSource();

                string imageUrl = $"https://www.mgm.gov.tr/sunum/tahmin-show-2.aspx?m={NormalizedName}&basla=1&bitir=5&rC=1E1E1E&rZ=FFFFFF";

                source.Html = $@"
                    <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <style>
                            body {{ 
                                margin: 0; 
                                padding: 0; 
                                background-color: #1E1E1E; 
                                display: flex; 
                                justify-content: center; 
                                align-items: center;
                                overflow: hidden; /* Kaydırma çubuklarını gizle */
                            }}
                            img {{ 
                                width: 100%;       /* Genişliği kapsayıcıya uydur */
                                height: auto;      /* Yüksekliği orantılı ayarla */
                                object-fit: contain; 
                                display: block;
                            }}
                        </style>
                    </head>
                    <body>
                        <img src='{imageUrl}' />
                    </body>
                    </html>";

                return source;
            }
        }
    }
}