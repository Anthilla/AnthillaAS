///-------------------------------------------------------------------------------------
///     Copyright (c) 2014, Anthilla S.r.l. (http://www.anthilla.com)
///     All rights reserved.
///
///     Redistribution and use in source and binary forms, with or without
///     modification, are permitted provided that the following conditions are met:
///         * Redistributions of source code must retain the above copyright
///           notice, this list of conditions and the following disclaimer.
///         * Redistributions in binary form must reproduce the above copyright
///           notice, this list of conditions and the following disclaimer in the
///           documentation and/or other materials provided with the distribution.
///         * Neither the name of the Anthilla S.r.l. nor the
///           names of its contributors may be used to endorse or promote products
///           derived from this software without specific prior written permission.
///
///     THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
///     ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
///     WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
///     DISCLAIMED. IN NO EVENT SHALL ANTHILLA S.R.L. BE LIABLE FOR ANY
///     DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
///     (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
///     LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
///     ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
///     (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
///     SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
///
///     20150413
///-------------------------------------------------------------------------------------

namespace AnthillaAS.Config {

    public class SelfConfig {
        private static string cfgAlias = "anthAsLiteControlSet";

        private static string[] paths = new string[] {
                cfgAlias + "Current",
                cfgAlias + "001",
                cfgAlias + "002"
            };

        private static Anth_ParamWriter config = new Anth_ParamWriter(paths, "anthAsLiteCore");

        public static void WriteDefaults() {
            string root;
            config.Write("root", "/anthAsLite");
            root = config.ReadValue("root");

            if (config.CheckValue("anthAsLiteport") == false) {
                config.Write("anthAsLiteport", "8084");
            }
        }

        public static string GetAntdPort() {
            return config.ReadValue("anthAsLiteport");
        }

        public static string GetAntdUri() {
            if (config.CheckValue("anthAsLitedb") == false) {
                return "http://+:8084/";
            }
            else {
                var port = config.ReadValue("anthAsLiteport");
                return "http://+:" + port + "/";
            }
        }
    }
}