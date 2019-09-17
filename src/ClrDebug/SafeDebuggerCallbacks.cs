using System;
using ClrDebug.Native;

namespace ClrDebug
{
    public abstract class SafeDebuggerCallbacks : ManagedDebuggerCallbacks
    {
        protected abstract int OnBreakpoint(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugBreakpoint pBreakpoint);

        protected abstract int OnStepComplete(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugStepper pStepper,
            CorDebugStepReason reason);

        protected abstract int OnBreak(CorDebugAppDomain pAppDomain, CorDebugThread thread);

        protected abstract int OnException(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            bool unhandled);

        protected abstract int OnEvalComplete(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugEval pEval);

        protected abstract int OnEvalException(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugEval pEval);

        protected abstract int OnCreateProcess(CorDebugProcess pProcess);

        protected abstract int OnExitProcess(CorDebugProcess pProcess);

        protected abstract int OnCreateThread(CorDebugAppDomain pAppDomain, CorDebugThread thread);

        protected abstract int OnExitThread(CorDebugAppDomain pAppDomain, CorDebugThread thread);

        protected abstract int OnLoadModule(CorDebugAppDomain pAppDomain, CorDebugModule pModule);

        protected abstract int OnUnloadModule(CorDebugAppDomain pAppDomain, CorDebugModule pModule);

        protected abstract int OnLoadClass(CorDebugAppDomain pAppDomain, CorDebugClass c);

        protected abstract int OnUnloadClass(CorDebugAppDomain pAppDomain, CorDebugClass c);

        protected abstract int OnDebuggerError(CorDebugProcess pProcess, int errorHR, uint errorCode);

        protected abstract int OnLogMessage(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            int lLevel,
            ReadOnlySpan<char> pLogSwitchName,
            ReadOnlySpan<char> pMessage);

        protected abstract int OnLogSwitch(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            int lLevel,
            uint ulReason,
            ReadOnlySpan<char> pLogSwitchName,
            ReadOnlySpan<char> pParentName);

        protected abstract int OnCreateAppDomain(CorDebugProcess pProcess, CorDebugAppDomain pAppDomain);

        protected abstract int OnExitAppDomain(CorDebugProcess pProcess, CorDebugAppDomain pAppDomain);

        protected abstract int OnLoadAssembly(CorDebugAppDomain pAppDomain, CorDebugAssembly pAssembly);

        protected abstract int OnUnloadAssembly(CorDebugAppDomain pAppDomain, CorDebugAssembly pAssembly);

        protected abstract int OnControlCTrap(CorDebugProcess pProcess);

        protected abstract int OnNameChange(CorDebugAppDomain pAppDomain, CorDebugThread pThread);

        protected abstract int OnUpdateModuleSymbols(
            CorDebugAppDomain pAppDomain,
            CorDebugModule pModule,
            IntPtr pSymbolStream);

        protected abstract int OnEditAndContinueRemap(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugFunction pFunction,
            bool fAccurate);

        protected abstract int OnBreakpointSetError(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugBreakpoint pBreakpoint,
            uint dwError);

        protected abstract int OnFunctionRemapOpportunity(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugFunction pOldFunction,
            CorDebugFunction pNewFunction,
            uint oldILOffset);

        protected abstract int OnCreateConnection(
            CorDebugProcess pProcess,
            uint dwConnectionId,
            ReadOnlySpan<char> pConnName);

        protected abstract int OnChangeConnection(CorDebugProcess pProcess, uint dwConnectionId);

        protected abstract int OnDestroyConnection(CorDebugProcess pProcess, uint dwConnectionId);

        protected abstract int OnException2(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugFrame pFrame,
            uint nOffset,
            CorDebugExceptionCallbackType dwEventType,
            uint dwFlags);

        protected abstract int OnExceptionUnwind(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugExceptionUnwindCallbackType dwEventType,
            uint dwFlags);

        protected abstract int OnFunctionRemapComplete(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugFunction pFunction);

        protected abstract int OnMDANotification(
            CorDebugController pController,
            CorDebugThread pThread,
            CorDebugMDA pMDA);

        protected override sealed unsafe int OnBreakpoint(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pBreakpoint)
            => OnBreakpoint(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugThread>(pThread),
                ComFactory.Create<CorDebugBreakpoint>(pBreakpoint));

        protected override sealed unsafe int OnStepComplete(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pStepper,
            int reason)
            => OnStepComplete(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugThread>(pThread),
                ComFactory.Create<CorDebugStepper>(pStepper),
                (CorDebugStepReason)reason);

        protected override sealed unsafe int OnBreak(void* @this, void* pAppDomain, void* thread)
            => OnBreak(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugThread>(thread));

        protected override sealed unsafe int OnException(
            void* @this,
            void* pAppDomain,
            void* pThread,
            bool unhandled)
            => OnException(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugThread>(pThread),
                unhandled);

        protected override sealed unsafe int OnEvalComplete(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pEval)
            => OnEvalComplete(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugThread>(pThread),
                ComFactory.Create<CorDebugEval>(pEval));

        protected override sealed unsafe int OnEvalException(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pEval)
            => OnEvalException(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugThread>(pThread),
                ComFactory.Create<CorDebugEval>(pEval));

        protected override sealed unsafe int OnCreateProcess(void* @this, void* pProcess)
            => OnCreateProcess(ComFactory.Create<CorDebugProcess>(pProcess));

        protected override sealed unsafe int OnExitProcess(void* @this, void* pProcess)
            => OnExitProcess(ComFactory.Create<CorDebugProcess>(pProcess));

        protected override sealed unsafe int OnCreateThread(void* @this, void* pAppDomain, void* thread)
            => OnCreateThread(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugThread>(thread));

        protected override sealed unsafe int OnExitThread(void* @this, void* pAppDomain, void* thread)
            => OnExitThread(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugThread>(thread));

        protected override sealed unsafe int OnLoadModule(void* @this, void* pAppDomain, void* pModule)
            => OnLoadModule(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugModule>(pModule));

        protected override sealed unsafe int OnUnloadModule(void* @this, void* pAppDomain, void* pModule)
            => OnUnloadModule(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugModule>(pModule));

        protected override sealed unsafe int OnLoadClass(void* @this, void* pAppDomain, void* c)
            => OnLoadClass(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugClass>(c));

        protected override sealed unsafe int OnUnloadClass(void* @this, void* pAppDomain, void* c)
            => OnUnloadClass(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugClass>(c));

        protected override sealed unsafe int OnDebuggerError(
            void* @this,
            void* pProcess,
            int errorHR,
            uint errorCode)
            => OnDebuggerError(
                ComFactory.Create<CorDebugProcess>(pProcess),
                errorHR,
                errorCode);

        protected override sealed unsafe int OnLogMessage(
            void* @this,
            void* pAppDomain,
            void* pThread,
            int lLevel,
            char* pLogSwitchName,
            char* pMessage)
            => OnLogMessage(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugThread>(pThread),
                lLevel,
                UnsafeOps.WCharToSpan(pLogSwitchName),
                UnsafeOps.WCharToSpan(pMessage));

        protected override sealed unsafe int OnLogSwitch(
            void* @this,
            void* pAppDomain,
            void* pThread,
            int lLevel,
            uint ulReason,
            char* pLogSwitchName,
            char* pParentName)
            => OnLogSwitch(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugThread>(pThread),
                lLevel,
                ulReason,
                UnsafeOps.WCharToSpan(pLogSwitchName),
                UnsafeOps.WCharToSpan(pParentName));

        protected override sealed unsafe int OnCreateAppDomain(void* @this, void* pProcess, void* pAppDomain)
            => OnCreateAppDomain(
                ComFactory.Create<CorDebugProcess>(pProcess),
                ComFactory.Create<CorDebugAppDomain>(pAppDomain));

        protected override sealed unsafe int OnExitAppDomain(void* @this, void* pProcess, void* pAppDomain)
            => OnExitAppDomain(
                ComFactory.Create<CorDebugProcess>(pProcess),
                ComFactory.Create<CorDebugAppDomain>(pAppDomain));

        protected override sealed unsafe int OnLoadAssembly(void* @this, void* pAppDomain, void* pAssembly)
            => OnLoadAssembly(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugAssembly>(pAssembly));

        protected override sealed unsafe int OnUnloadAssembly(void* @this, void* pAppDomain, void* pAssembly)
            => OnUnloadAssembly(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugAssembly>(pAssembly));

        protected override sealed unsafe int OnControlCTrap(void* @this, void* pProcess)
            => OnControlCTrap(ComFactory.Create<CorDebugProcess>(pProcess));

        protected override sealed unsafe int OnNameChange(void* @this, void* pAppDomain, void* pThread)
            => OnNameChange(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugThread>(pThread));

        protected override sealed unsafe int OnUpdateModuleSymbols(
            void* @this,
            void* pAppDomain,
            void* pModule,
            void* pSymbolStream)
            => OnUpdateModuleSymbols(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugModule>(pModule),
                new IntPtr(pSymbolStream));

        protected override sealed unsafe int OnEditAndContinueRemap(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pFunction,
            bool fAccurate)
            => OnEditAndContinueRemap(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugThread>(pThread),
                ComFactory.Create<CorDebugFunction>(pFunction),
                fAccurate);

        protected override sealed unsafe int OnBreakpointSetError(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pBreakpoint,
            uint dwError)
                => OnBreakpointSetError(
                    ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                    ComFactory.Create<CorDebugThread>(pThread),
                    ComFactory.Create<CorDebugBreakpoint>(pBreakpoint),
                    dwError);

        protected override sealed unsafe int OnFunctionRemapOpportunity(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pOldFunction,
            void* pNewFunction,
            uint oldILOffset)
                => OnFunctionRemapOpportunity(
                    ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                    ComFactory.Create<CorDebugThread>(pThread),
                    ComFactory.Create<CorDebugFunction>(pOldFunction),
                    ComFactory.Create<CorDebugFunction>(pNewFunction),
                    oldILOffset);

        protected override sealed unsafe int OnCreateConnection(
            void* @this,
            void* pProcess,
            uint dwConnectionId,
            char* pConnName)
            => OnCreateConnection(
                ComFactory.Create<CorDebugProcess>(pProcess),
                dwConnectionId,
                UnsafeOps.WCharToSpan(pConnName));

        protected override sealed unsafe int OnChangeConnection(
            void* @this,
            void* pProcess,
            uint dwConnectionId)
            => OnChangeConnection(
                ComFactory.Create<CorDebugProcess>(pProcess),
                dwConnectionId);

        protected override sealed unsafe int OnDestroyConnection(
            void* @this,
            void* pProcess,
            uint dwConnectionId)
            => OnDestroyConnection(
                ComFactory.Create<CorDebugProcess>(pProcess),
                dwConnectionId);

        protected override sealed unsafe int OnException2(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pFrame,
            uint nOffset,
            int dwEventType,
            uint dwFlags)
            => OnException2(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugThread>(pThread),
                ComFactory.Create<CorDebugFrame>(pFrame),
                nOffset,
                (CorDebugExceptionCallbackType)dwEventType,
                dwFlags);

        protected override sealed unsafe int OnExceptionUnwind(
            void* @this,
            void* pAppDomain,
            void* pThread,
            int dwEventType,
            uint dwFlags)
            => OnExceptionUnwind(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugThread>(pThread),
                (CorDebugExceptionUnwindCallbackType)dwEventType,
                dwFlags);

        protected override sealed unsafe int OnFunctionRemapComplete(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pFunction)
            => OnFunctionRemapComplete(
                ComFactory.Create<CorDebugAppDomain>(pAppDomain),
                ComFactory.Create<CorDebugThread>(pThread),
                ComFactory.Create<CorDebugFunction>(pFunction));

        protected override sealed unsafe int OnMDANotification(
            void* @this,
            void* pController,
            void* pThread,
            void* pMDA)
            => OnMDANotification(
                ComFactory.Create<CorDebugController>(pController),
                ComFactory.Create<CorDebugThread>(pThread),
                ComFactory.Create<CorDebugMDA>(pMDA));
    }
}
