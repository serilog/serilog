(function () {

    window.$log = log;
    window.$load_module = load_module;
    window.$initialize_hosted_projections = initialize_hosted_projections;

    function load_module(module_name) {
        // just a temporary spike. need to load modules from server
        if (module_name == "Modules") {
            return $modules;
        }
        if (module_name == "Projections") {
            return $projections;
        }
        throw "Module not found exception: " + module_name;
    };

    function initialize_hosted_projections() {
        var eventProcessor = {};

        window.$on = function (name, handler) {
            eventProcessor[name] = handler;
        };
        
        window.$notify = function (name, data) { write(">" + name + " : " + data); };
        
        var global = scope($on, $notify);
        for (var prop in global) {
            window[prop] = global[prop];
        }

        return eventProcessor;
    }

    function log(data) {
        write("log: " + data);
    }
    
    function write(text) {
        document.getElementById("text").innerHTML += ["<pre>", text, "</pre>"].join("");         
    }

})();