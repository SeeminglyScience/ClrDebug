namespace ClrDebug
{
    internal class LoggerManagedCallbacks : ManagedDebuggerCallbacks
    {
        protected override unsafe int OnBreak(void* @this, void* pAppDomain, void* thread)
        {
            return LogAndContinue(nameof(OnBreak));
        }

        protected override unsafe int OnBreakpoint(void* @this, void* pAppDomain, void* pThread, void* pBreakpoint)
        {
            return LogAndContinue(nameof(OnBreakpoint));
        }

        protected override unsafe int OnBreakpointSetError(void* @this, void* pAppDomain, void* pThread, void* pBreakpoint, uint dwError)
        {
            return LogAndContinue(nameof(OnBreakpointSetError));
        }

        protected override unsafe int OnChangeConnection(void* @this, void* pProcess, uint dwConnectionId)
        {
            return LogAndContinue(nameof(OnChangeConnection));
        }

        protected override unsafe int OnControlCTrap(void* @this, void* pProcess)
        {
            return LogAndContinue(nameof(OnControlCTrap));
        }

        protected override unsafe int OnCreateAppDomain(void* @this, void* pProcess, void* pAppDomain)
        {
            return LogAndContinue(nameof(OnCreateAppDomain));
        }

        protected override unsafe int OnCreateConnection(void* @this, void* pProcess, uint dwConnectionId, char* pConnName)
        {
            return LogAndContinue(nameof(OnCreateConnection));
        }

        protected override unsafe int OnCreateProcess(void* @this, void* pProcess)
        {
            return LogAndContinue(nameof(OnCreateProcess));
        }

        protected override unsafe int OnCreateThread(void* @this, void* pAppDomain, void* thread)
        {
            return LogAndContinue(nameof(OnCreateThread));
        }

        protected override unsafe int OnDebuggerError(void* @this, void* pProcess, int errorHR, uint errorCode)
        {
            return LogAndContinue(nameof(OnDebuggerError));
        }

        protected override unsafe int OnDestroyConnection(void* @this, void* pProcess, uint dwConnectionId)
        {
            return LogAndContinue(nameof(OnDestroyConnection));
        }

        protected override unsafe int OnEditAndContinueRemap(void* @this, void* pAppDomain, void* pThread, void* pFunction, bool fAccurate)
        {
            return LogAndContinue(nameof(OnEditAndContinueRemap));
        }

        protected override unsafe int OnEvalComplete(void* @this, void* pAppDomain, void* pThread, void* pEval)
        {
            return LogAndContinue(nameof(OnEvalComplete));
        }

        protected override unsafe int OnEvalException(void* @this, void* pAppDomain, void* pThread, void* pEval)
        {
            return LogAndContinue(nameof(OnEvalException));
        }

        protected override unsafe int OnException(void* @this, void* pAppDomain, void* pThread, bool unhandled)
        {
            LogAndContinue(nameof(OnException));
            return 0;
        }

        protected override unsafe int OnException2(void* @this, void* pAppDomain, void* pThread, void* pFrame, uint nOffset, int dwEventType, uint dwFlags)
        {
            return LogAndContinue(nameof(OnException2));
        }

        protected override unsafe int OnExceptionUnwind(void* @this, void* pAppDomain, void* pThread, int dwEventType, uint dwFlags)
        {
            return LogAndContinue(nameof(OnExceptionUnwind));
        }

        protected override unsafe int OnExitAppDomain(void* @this, void* pProcess, void* pAppDomain)
        {
            return LogAndContinue(nameof(OnExitAppDomain));
        }

        protected override unsafe int OnExitProcess(void* @this, void* pProcess)
        {
            return LogAndContinue(nameof(OnExitProcess));
        }

        protected override unsafe int OnExitThread(void* @this, void* pAppDomain, void* thread)
        {
            return LogAndContinue(nameof(OnExitThread));
        }

        protected override unsafe int OnFunctionRemapComplete(void* @this, void* pAppDomain, void* pThread, void* pFunction)
        {
            return LogAndContinue(nameof(OnFunctionRemapComplete));
        }

        protected override unsafe int OnFunctionRemapOpportunity(void* @this, void* pAppDomain, void* pThread, void* pOldFunction, void* pNewFunction, uint oldILOffset)
        {
            return LogAndContinue(nameof(OnFunctionRemapOpportunity));
        }

        protected override unsafe int OnLoadAssembly(void* @this, void* pAppDomain, void* pAssembly)
        {
            return LogAndContinue(nameof(OnLoadAssembly));
        }

        protected override unsafe int OnLoadClass(void* @this, void* pAppDomain, void* c)
        {
            return LogAndContinue(nameof(OnLoadClass));
        }

        protected override unsafe int OnLoadModule(void* @this, void* pAppDomain, void* pModule)
        {
            return LogAndContinue(nameof(OnLoadModule));
        }

        protected override unsafe int OnLogMessage(void* @this, void* pAppDomain, void* pThread, int lLevel, char* pLogSwitchName, char* pMessage)
        {
            return LogAndContinue(nameof(OnLogMessage));
        }

        protected override unsafe int OnLogSwitch(void* @this, void* pAppDomain, void* pThread, int lLevel, uint ulReason, char* pLogSwitchName, char* pParentName)
        {
            return LogAndContinue(nameof(OnLogSwitch));
        }

        protected override unsafe int OnMDANotification(void* @this, void* pController, void* pThread, void* pMDA)
        {
            return LogAndContinue(nameof(OnMDANotification));
        }

        protected override unsafe int OnNameChange(void* @this, void* pAppDomain, void* pThread)
        {
            return LogAndContinue(nameof(OnNameChange));
        }

        protected override unsafe int OnStepComplete(void* @this, void* pAppDomain, void* pThread, void* pStepper, int reason)
        {
            return LogAndContinue(nameof(OnStepComplete));
        }

        protected override unsafe int OnUnloadAssembly(void* @this, void* pAppDomain, void* pAssembly)
        {
            return LogAndContinue(nameof(OnUnloadAssembly));
        }

        protected override unsafe int OnUnloadClass(void* @this, void* pAppDomain, void* c)
        {
            return LogAndContinue(nameof(OnUnloadClass));
        }

        protected override unsafe int OnUnloadModule(void* @this, void* pAppDomain, void* pModule)
        {
            return LogAndContinue(nameof(OnUnloadModule));
        }

        protected override unsafe int OnUpdateModuleSymbols(void* @this, void* pAppDomain, void* pModule, void* pSymbolStream)
        {
            return LogAndContinue(nameof(OnUpdateModuleSymbols));
        }

        private int LogAndContinue(string methodName)
        {
            System.Console.WriteLine(methodName);
            return Continue();
        }
    }
}
