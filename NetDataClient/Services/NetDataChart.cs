namespace RedBoarder.NetDataClient.Dtos
{
    public class NetDataChart
    {
        public string Name { get; }
        public string Value { get; }
        private NetDataChart(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
        /// <summary>
        /// system.cpu
        /// </summary>
        public static NetDataChart CPU => new("CPU", "system.cpu");


        /// <summary>
        /// system.ram
        /// </summary>
        public static NetDataChart RAM => new("RAM", "system.ram");

        /// <summary>
        /// system.net
        /// </summary>
        public static NetDataChart NET => new("NET", "system.net");





        /// <summary>
        ///  web_log_nginx.response_statuses : Web server responses by type. success includes 1xx, 2xx, 304 and 401,
        ///  error includes 5xx, redirect includes 3xx except 304, bad includes 4xx except 401, other are all the other responses.
        /// </summary>
        public static NetDataChart NGINX_RESPONSE_STATUSES => new("NGINX_RESPONSE_STATUS", "web_log_nginx.response_statuses");


        /// <summary>
        /// web_log_nginx.response_codes : Web server responses by code family.
        /// According to the standards 1xx are informational responses, 2xx are successful responses,
        /// 3xx are redirects (although they include 304 which is used as "not modified"), 4xx are bad requests,
        /// 5xx are internal server errors, other are non-standard responses,
        /// unmatched counts the lines in the log file that are not matched by the plugin (let us know if you have any unmatched).
        /// </summary>
        public static NetDataChart NGINX_RESPONSE_CODES => new("NGINX_RESPONSE_CODES", "web_log_nginx.response_codes");
        /// <summary>
        /// web_log_nginx.detailed_response_codes : Number of responses for each response code individually.
        /// </summary>
        public static NetDataChart NGINX_RESPONSE_DETAILS => new("NGINX_RESPONSE_DETAILS", "web_log_nginx.detailed_response_codes");

        /// <summary>
        /// web_log_nginx.bandwidth
        /// </summary>
        public static NetDataChart NGINX_BANDWIDTH => new("NGINX_RESPONSE_DETAILS", "web_log_nginx.bandwidth");

        /// <summary>
        /// web_log_nginx.clients : Charts showing the number of unique client IPs, accessing the web server.
        /// </summary>
        public static NetDataChart NGINX_CIENTS => new("NGINX_CIENTS", "web_log_nginx.clients");



        public static NetDataChart IPV4_SOCKETS => new("IPV4_SOCKETS", "ipv4.sockstat_sockets");


        public static NetDataChart IPV4_PACKETS => new("IPV4_PACKETS", "ipv4.packets");


        public static NetDataChart IPV4_ERRORS => new("IPV4_ERRORS", "ipv4.errors");


        public static NetDataChart IPV4_TCPSOCK => new("IPV4_ERRORS", "ipv4.tcpsock");


    }
}