using System;
using ClrDebug.Native;

namespace ClrDebug
{
    internal sealed class EventManagedCallbacks : SafeDebuggerCallbacks
    {
        public delegate void ByRefEventHandler<T>(object sender, T arg);

        public delegate void ByRefEventHandler<T, T1>(object sender, T arg0, T1 arg1);

        public delegate void ByRefEventHandler<T, T1, T2>(object sender, T arg0, T1 arg1, T2 arg2);

        public event ByRefEventHandler<CorDebugAppDomain, CorDebugThread> Break;

        protected override unsafe int OnBreak(CorDebugAppDomain pAppDomain, CorDebugThread thread)
        {
            Break?.Invoke(this, pAppDomain, thread);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugAppDomain, CorDebugThread, CorDebugBreakpoint> Breakpoint;

        protected override unsafe int OnBreakpoint(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugBreakpoint pBreakpoint)
        {
            Breakpoint?.Invoke(this, pAppDomain, pThread, pBreakpoint);
            return Continue();
        }

        public delegate void BreakpointSetErrorEventHandler(
            object sender,
            CorDebugAppDomain appDomain,
            CorDebugThread thread,
            CorDebugBreakpoint breakpoint,
            uint error);

        public event BreakpointSetErrorEventHandler BreakpointSetError;

        protected override unsafe int OnBreakpointSetError(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugBreakpoint pBreakpoint,
            uint dwError)
        {
            BreakpointSetError?.Invoke(this, pAppDomain, pThread, pBreakpoint, dwError);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugProcess> ControlCEvent;

        protected override unsafe int OnControlCTrap(CorDebugProcess pProcess)
        {
            ControlCEvent?.Invoke(this, pProcess);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugProcess, CorDebugAppDomain> CreateAppDomain;

        protected override unsafe int OnCreateAppDomain(CorDebugProcess pProcess, CorDebugAppDomain pAppDomain)
        {
            CreateAppDomain?.Invoke(this, pProcess, pAppDomain);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugProcess> CreateProcess;

        protected override unsafe int OnCreateProcess(CorDebugProcess pProcess)
        {
            CreateProcess?.Invoke(this, pProcess);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugAppDomain, CorDebugThread> CreateThread;

        protected override unsafe int OnCreateThread(CorDebugAppDomain pAppDomain, CorDebugThread thread)
        {
            CreateThread?.Invoke(this, pAppDomain, thread);
            return Continue();
        }

        public delegate void DebuggerErrorEventHandler(
            object sender,
            CorDebugProcess process,
            int errorHR,
            uint errorCode);

        public event DebuggerErrorEventHandler DebuggerError;

        protected override unsafe int OnDebuggerError(CorDebugProcess pProcess, int errorHR, uint errorCode)
        {
            DebuggerError?.Invoke(this, pProcess, errorHR, errorCode);
            return Continue();
        }

        public delegate void EditAndContinueRemapEventHandler(
            object sender,
            CorDebugAppDomain appDomain,
            CorDebugThread thread,
            CorDebugFunction function,
            bool isAccurate);

        public event EditAndContinueRemapEventHandler EditAndContinueRemap;

        protected override unsafe int OnEditAndContinueRemap(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugFunction pFunction,
            bool fAccurate)
        {
            EditAndContinueRemap?.Invoke(this, pAppDomain, pThread, pFunction, fAccurate);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugAppDomain, CorDebugThread, CorDebugEval> EvalComplete;

        protected override unsafe int OnEvalComplete(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugEval pEval)
        {
            EvalComplete?.Invoke(this, pAppDomain, pThread, pEval);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugAppDomain, CorDebugThread, CorDebugEval> EvalException;

        protected override unsafe int OnEvalException(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugEval pEval)
        {
            EvalException?.Invoke(this, pAppDomain, pThread, pEval);
            return Continue();
        }

        public delegate void ExceptionEventHandler(
            object sender,
            CorDebugAppDomain appDomain,
            CorDebugThread thread,
            bool isUnhandled);

        public event ExceptionEventHandler Exception;

        protected override unsafe int OnException(CorDebugAppDomain pAppDomain, CorDebugThread pThread, bool unhandled)
        {
            Exception?.Invoke(this, pAppDomain, pThread, unhandled);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugProcess, CorDebugAppDomain> ExitAppDomain;

        protected override unsafe int OnExitAppDomain(CorDebugProcess pProcess, CorDebugAppDomain pAppDomain)
        {
            ExitAppDomain?.Invoke(this, pProcess, pAppDomain);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugProcess> ExitProcess;

        protected override unsafe int OnExitProcess(CorDebugProcess pProcess)
        {
            ExitProcess?.Invoke(this, pProcess);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugAppDomain, CorDebugThread> ExitThread;

        protected override unsafe int OnExitThread(CorDebugAppDomain pAppDomain, CorDebugThread thread)
        {
            ExitThread?.Invoke(this, pAppDomain, thread);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugAppDomain, CorDebugAssembly> LoadAssembly;

        protected override unsafe int OnLoadAssembly(CorDebugAppDomain pAppDomain, CorDebugAssembly pAssembly)
        {
            LoadAssembly?.Invoke(this, pAppDomain, pAssembly);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugAppDomain, CorDebugClass> LoadClass;

        protected override unsafe int OnLoadClass(CorDebugAppDomain pAppDomain, CorDebugClass c)
        {
            LoadClass?.Invoke(this, pAppDomain, c);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugAppDomain, CorDebugModule> LoadModule;

        protected override unsafe int OnLoadModule(CorDebugAppDomain pAppDomain, CorDebugModule pModule)
        {
            LoadModule?.Invoke(this, pAppDomain, pModule);
            return Continue();
        }

        public delegate void LogMessageEventHandler(
            object sender,
            CorDebugAppDomain appDomain,
            CorDebugThread thread,
            int level,
            ReadOnlySpan<char> logSwitchName,
            ReadOnlySpan<char> message);

        public event LogMessageEventHandler LogMessage;

        protected override unsafe int OnLogMessage(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            int lLevel,
            ReadOnlySpan<char> pLogSwitchName,
            ReadOnlySpan<char> pMessage)
        {
            LogMessage?.Invoke(
                this,
                pAppDomain,
                pThread,
                lLevel,
                pLogSwitchName,
                pMessage);

            return Continue();
        }

        public delegate void LogSwitchEventHandler(
            object sender,
            CorDebugAppDomain appDomain,
            CorDebugThread thread,
            int level,
            uint reason,
            ReadOnlySpan<char> logSwitchName,
            ReadOnlySpan<char> parentName);

        public event LogSwitchEventHandler LogSwitch;

        protected override unsafe int OnLogSwitch(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            int lLevel,
            uint ulReason,
            ReadOnlySpan<char> pLogSwitchName,
            ReadOnlySpan<char> pParentName)
        {
            LogSwitch?.Invoke(
                this,
                pAppDomain,
                pThread,
                lLevel,
                ulReason,
                pLogSwitchName,
                pParentName);

            return Continue();
        }

        public event ByRefEventHandler<CorDebugAppDomain, CorDebugThread> NameChange;

        protected override unsafe int OnNameChange(CorDebugAppDomain pAppDomain, CorDebugThread pThread)
        {
            NameChange?.Invoke(this, pAppDomain, pThread);
            return Continue();
        }

        public delegate void StepCompleteEventHandler(
            object sender,
            CorDebugAppDomain appDomain,
            CorDebugThread thread,
            CorDebugStepper stepper,
            CorDebugStepReason reason);

        public event StepCompleteEventHandler StepComplete;

        protected override unsafe int OnStepComplete(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugStepper pStepper,
            CorDebugStepReason reason)
        {
            StepComplete?.Invoke(this, pAppDomain, pThread, pStepper, reason);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugAppDomain, CorDebugAssembly> UnloadAssembly;

        protected override unsafe int OnUnloadAssembly(CorDebugAppDomain pAppDomain, CorDebugAssembly pAssembly)
        {
            UnloadAssembly?.Invoke(this, pAppDomain, pAssembly);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugAppDomain, CorDebugClass> UnloadClass;

        protected override unsafe int OnUnloadClass(CorDebugAppDomain pAppDomain, CorDebugClass c)
        {
            UnloadClass?.Invoke(this, pAppDomain, c);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugAppDomain, CorDebugModule> UnloadModule;

        protected override unsafe int OnUnloadModule(CorDebugAppDomain pAppDomain, CorDebugModule pModule)
        {
            UnloadModule?.Invoke(this, pAppDomain, pModule);
            return Continue();
        }

        public delegate void UpdateModuleSymbolsEventHandler(
            object sender,
            CorDebugAppDomain appDomain,
            CorDebugModule pModule,
            IntPtr symbolStream);

        public event UpdateModuleSymbolsEventHandler UpdateModuleSymbols;

        protected override unsafe int OnUpdateModuleSymbols(
            CorDebugAppDomain pAppDomain,
            CorDebugModule pModule,
            IntPtr pSymbolStream)
        {
            UpdateModuleSymbols?.Invoke(this, pAppDomain, pModule, pSymbolStream);
            return Continue();
        }

        public delegate void FunctionRemapOpportunityEventHandler(
            object sender,
            CorDebugAppDomain appDomain,
            CorDebugThread thread,
            CorDebugFunction oldFunction,
            CorDebugFunction newFunction,
            long oldILOffset);

        public event FunctionRemapOpportunityEventHandler FunctionRemapOpportunity;

        protected override unsafe int OnFunctionRemapOpportunity(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugFunction pOldFunction,
            CorDebugFunction pNewFunction,
            uint oldILOffset)
        {
            FunctionRemapOpportunity?.Invoke(
                this,
                pAppDomain,
                pThread,
                pOldFunction,
                pNewFunction,
                oldILOffset);

            return Continue();
        }

        public delegate void CreateConnectionEventHandler(
            object sender,
            CorDebugProcess process,
            long connectionId,
            ReadOnlySpan<char> connName);

        public event CreateConnectionEventHandler CreateConnection;

        protected override unsafe int OnCreateConnection(
            CorDebugProcess pProcess,
            uint dwConnectionId,
            ReadOnlySpan<char> pConnName)
        {
            CreateConnection?.Invoke(this, pProcess, dwConnectionId, pConnName);
            return Continue();
        }

        public delegate void ConnectionEventHandler(
            object sender,
            CorDebugProcess process,
            long connectionId);

        public event ConnectionEventHandler ChangeConnection;

        protected override unsafe int OnChangeConnection(CorDebugProcess pProcess, uint dwConnectionId)
        {
            ChangeConnection?.Invoke(this, pProcess, dwConnectionId);
            return Continue();
        }

        public event ConnectionEventHandler DestroyConnection;

        protected override unsafe int OnDestroyConnection(CorDebugProcess pProcess, uint dwConnectionId)
        {
            DestroyConnection?.Invoke(this, pProcess, dwConnectionId);
            return Continue();
        }

        public delegate void Exception2EventHandler(
            object sender,
            CorDebugAppDomain appDomain,
            CorDebugThread thread,
            CorDebugFrame frame,
            long offset,
            CorDebugExceptionCallbackType eventType,
            long flags);

        public event Exception2EventHandler Exception2;

        protected override unsafe int OnException2(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugFrame pFrame,
            uint nOffset,
            CorDebugExceptionCallbackType dwEventType,
            uint dwFlags)
        {
            Exception2?.Invoke(this, pAppDomain, pThread, pFrame, nOffset, dwEventType, dwFlags);
            return Continue();
        }

        public delegate void ExceptionUnwindEventHandler(
            object sender,
            CorDebugAppDomain appDomain,
            CorDebugThread thread,
            CorDebugExceptionUnwindCallbackType eventType,
            long flags);

        public event ExceptionUnwindEventHandler ExceptionUnwind;

        protected override unsafe int OnExceptionUnwind(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugExceptionUnwindCallbackType dwEventType,
            uint dwFlags)
        {
            ExceptionUnwind?.Invoke(this, pAppDomain, pThread, dwEventType, dwFlags);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugAppDomain, CorDebugThread, CorDebugFunction> FunctionRemapComplete;

        protected override unsafe int OnFunctionRemapComplete(
            CorDebugAppDomain pAppDomain,
            CorDebugThread pThread,
            CorDebugFunction pFunction)
        {
            FunctionRemapComplete?.Invoke(this, pAppDomain, pThread, pFunction);
            return Continue();
        }

        public event ByRefEventHandler<CorDebugController, CorDebugThread, CorDebugMDA> MDANotification;

        protected override unsafe int OnMDANotification(
            CorDebugController pController,
            CorDebugThread pThread,
            CorDebugMDA pMDA)
        {
            MDANotification?.Invoke(this, pController, pThread, pMDA);
            return Continue();
        }
    }
}
