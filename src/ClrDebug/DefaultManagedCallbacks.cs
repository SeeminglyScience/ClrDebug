using ClrDebug.Native;

namespace ClrDebug
{
    internal class DefaultManagedCallbacks : ManagedDebuggerCallbacks
    {
        protected override unsafe int OnBreak(void* @this, void* pAppDomain, void* thread)
            => Continue();

        protected override unsafe int OnBreakpoint(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pBreakpoint)
            => Continue();

        protected override unsafe int OnBreakpointSetError(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pBreakpoint,
            uint dwError)
            => Continue();

        protected override unsafe int OnChangeConnection(void* @this, void* pProcess, uint dwConnectionId)
            => Continue();

        protected override unsafe int OnControlCTrap(void* @this, void* pProcess)
            => Continue();

        protected override unsafe int OnCreateAppDomain(void* @this, void* pProcess, void* pAppDomain)
            => Continue();

        protected override unsafe int OnCreateConnection(
            void* @this,
            void* pProcess,
            uint dwConnectionId,
            char* pConnName)
            => Continue();

        protected override unsafe int OnCreateProcess(void* @this, void* pProcess)
            => Continue();

        protected override unsafe int OnCreateThread(void* @this, void* pAppDomain, void* thread)
            => Continue();

        protected override unsafe int OnDebuggerError(void* @this, void* pProcess, int errorHR, uint errorCode)
            => Continue();

        protected override unsafe int OnDestroyConnection(void* @this, void* pProcess, uint dwConnectionId)
            => Continue();

        protected override unsafe int OnEditAndContinueRemap(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pFunction,
            bool fAccurate)
            => Continue();

        protected override unsafe int OnEvalComplete(void* @this, void* pAppDomain, void* pThread, void* pEval)
            => Continue();

        protected override unsafe int OnEvalException(void* @this, void* pAppDomain, void* pThread, void* pEval)
            => Continue();

        protected override unsafe int OnException(void* @this, void* pAppDomain, void* pThread, bool unhandled)
            => Continue();

        protected override unsafe int OnException2(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pFrame,
            uint nOffset,
            int dwEventType,
            uint dwFlags)
            => Continue();

        protected override unsafe int OnExceptionUnwind(
            void* @this,
            void* pAppDomain,
            void* pThread,
            int dwEventType,
            uint dwFlags)
            => Continue();

        protected override unsafe int OnExitAppDomain(void* @this, void* pProcess, void* pAppDomain)
            => Continue();

        protected override unsafe int OnExitProcess(void* @this, void* pProcess)
            => Continue();

        protected override unsafe int OnExitThread(void* @this, void* pAppDomain, void* thread)
            => Continue();

        protected override unsafe int OnFunctionRemapComplete(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pFunction)
            => Continue();

        protected override unsafe int OnFunctionRemapOpportunity(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pOldFunction,
            void* pNewFunction,
            uint oldILOffset)
            => Continue();

        protected override unsafe int OnLoadAssembly(void* @this, void* pAppDomain, void* pAssembly)
            => Continue();

        protected override unsafe int OnLoadClass(void* @this, void* pAppDomain, void* c)
            => Continue();

        protected override unsafe int OnLoadModule(void* @this, void* pAppDomain, void* pModule)
            => Continue();

        protected override unsafe int OnLogMessage(
            void* @this,
            void* pAppDomain,
            void* pThread,
            int lLevel,
            char* pLogSwitchName,
            char* pMessage)
            => Continue();

        protected override unsafe int OnLogSwitch(
            void* @this,
            void* pAppDomain,
            void* pThread,
            int lLevel,
            uint ulReason,
            char* pLogSwitchName,
            char* pParentName)
            => Continue();

        protected override unsafe int OnMDANotification(void* @this, void* pController, void* pThread, void* pMDA)
            => Continue();

        protected override unsafe int OnNameChange(void* @this, void* pAppDomain, void* pThread)
            => Continue();

        protected override unsafe int OnStepComplete(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pStepper,
            int reason)
            => Continue();

        protected override unsafe int OnUnloadAssembly(void* @this, void* pAppDomain, void* pAssembly)
            => Continue();

        protected override unsafe int OnUnloadClass(void* @this, void* pAppDomain, void* c)
            => Continue();

        protected override unsafe int OnUnloadModule(void* @this, void* pAppDomain, void* pModule)
            => Continue();

        protected override unsafe int OnUpdateModuleSymbols(
            void* @this,
            void* pAppDomain,
            void* pModule,
            void* pSymbolStream)
            => Continue();
    }
}
