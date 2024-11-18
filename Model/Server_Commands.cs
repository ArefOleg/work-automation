public static class Server_Commands{
    public static string setLog(string compName, string serverName, stirng logLvl){
        string log = $"change evtloglvl % = {logLvl} for server {serverName} component {compName}";
        return log;
    }

    public static getSessionForComp(string compName, string serverName){
        string session = $"list session for comp {compName} server {serverName}";
        return session;
    }

    public static getParamFromComp(string compName, string serverName){
        string param = $"list param %Branch% for comp {compName} server {serverName}";
        return param;
    }

    public static changeBrancheForComp(string compName, string serverName, string branch){
        string param = $"change param WorkspaceBranchName={branch} for comp {compName} server {serverName}";
        return param;
    }
}