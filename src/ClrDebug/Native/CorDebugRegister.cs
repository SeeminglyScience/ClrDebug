namespace ClrDebug.Native
{
    public enum CorDebugRegister
    {
        /// <summary>An instruction pointer register on any processor.<summary>
        REGISTER_INSTRUCTION_POINTER = 0,

        /// <summary>A stack pointer register on any processor.<summary>
        REGISTER_STACK_POINTER = REGISTER_INSTRUCTION_POINTER + 1,

        /// <summary>A frame pointer register on any processor.<summary>
        REGISTER_FRAME_POINTER = REGISTER_STACK_POINTER + 1,

        /// <summary>The instruction pointer register on the x86 processor.<summary>
        REGISTER_X86_EIP = 0,

        /// <summary>The stack pointer register on the x86 processor.<summary>
        REGISTER_X86_ESP = REGISTER_X86_EIP + 1,

        /// <summary>The base pointer register on the x86 processor.<summary>
        REGISTER_X86_EBP = REGISTER_X86_ESP + 1,

        /// <summary>The A data register on the x86 processor.<summary>
        REGISTER_X86_EAX = REGISTER_X86_EBP + 1,

        /// <summary>The C data register on the x86 processor.<summary>
        REGISTER_X86_ECX = REGISTER_X86_EAX + 1,

        /// <summary>The D data register on the x86 processor.<summary>
        REGISTER_X86_EDX = REGISTER_X86_ECX + 1,

        /// <summary>The B data register on the x86 processor.<summary>
        REGISTER_X86_EBX = REGISTER_X86_EDX + 1,

        /// <summary>The source index register on the x86 processor.<summary>
        REGISTER_X86_ESI = REGISTER_X86_EBX + 1,

        /// <summary>The destination index register on the x86 processor.<summary>
        REGISTER_X86_EDI = REGISTER_X86_ESI + 1,

        /// <summary>The stack register 0 on the x86 floating-point (FP) processor.<summary>
        REGISTER_X86_FPSTACK_0 = REGISTER_X86_EDI + 1,

        /// <summary>The #1 stack register on the x86 FP processor.<summary>
        REGISTER_X86_FPSTACK_1 = REGISTER_X86_FPSTACK_0 + 1,

        /// <summary>The #2 stack register on the x86 FP processor.<summary>
        REGISTER_X86_FPSTACK_2 = REGISTER_X86_FPSTACK_1 + 1,

        /// <summary>The #3 stack register on the x86 FP processor.<summary>
        REGISTER_X86_FPSTACK_3 = REGISTER_X86_FPSTACK_2 + 1,

        /// <summary>The #4 stack register on the x86 FP processor.<summary>
        REGISTER_X86_FPSTACK_4 = REGISTER_X86_FPSTACK_3 + 1,

        /// <summary>The #5 stack register on the x86 FP processor.<summary>
        REGISTER_X86_FPSTACK_5 = REGISTER_X86_FPSTACK_4 + 1,

        /// <summary>The #6 stack register on the x86 FP processor.<summary>
        REGISTER_X86_FPSTACK_6 = REGISTER_X86_FPSTACK_5 + 1,

        /// <summary>The #7 stack register on the x86 FP processor.<summary>
        REGISTER_X86_FPSTACK_7 = REGISTER_X86_FPSTACK_6 + 1,

        /// <summary>The instruction pointer register on the AMD64 processor.<summary>
        REGISTER_AMD64_RIP = 0,

        /// <summary>The stack pointer register on the AMD64 processor.<summary>
        REGISTER_AMD64_RSP = REGISTER_AMD64_RIP + 1,

        /// <summary>The base pointer register on the AMD64 processor.<summary>
        REGISTER_AMD64_RBP = REGISTER_AMD64_RSP + 1,

        /// <summary>The A data register on the AMD64 processor.<summary>
        REGISTER_AMD64_RAX = REGISTER_AMD64_RBP + 1,

        /// <summary>The C data register on the AMD64 processor.<summary>
        REGISTER_AMD64_RCX = REGISTER_AMD64_RAX + 1,

        /// <summary>The D data register on the AMD64 processor.<summary>
        REGISTER_AMD64_RDX = REGISTER_AMD64_RCX + 1,

        /// <summary>The B data register on the AMD64 processor.<summary>
        REGISTER_AMD64_RBX = REGISTER_AMD64_RDX + 1,

        /// <summary>The source index register on the AMD64 processor.<summary>
        REGISTER_AMD64_RSI = REGISTER_AMD64_RBX + 1,

        /// <summary>The destination index register on the AMD64 processor.<summary>
        REGISTER_AMD64_RDI = REGISTER_AMD64_RSI + 1,

        /// <summary>The #8 data register on the AMD64 processor.<summary>
        REGISTER_AMD64_R8 = REGISTER_AMD64_RDI + 1,

        /// <summary>The #9 data register on the AMD64 processor.<summary>
        REGISTER_AMD64_R9 = REGISTER_AMD64_R8 + 1,

        /// <summary>The #10 data register on the AMD64 processor.<summary>
        REGISTER_AMD64_R10 = REGISTER_AMD64_R9 + 1,

        /// <summary>The #11 data register on the AMD64 processor.<summary>
        REGISTER_AMD64_R11 = REGISTER_AMD64_R10 + 1,

        /// <summary>The #12 data register on the AMD64 processor.<summary>
        REGISTER_AMD64_R12 = REGISTER_AMD64_R11 + 1,

        /// <summary>The #13 data register on the AMD64 processor.<summary>
        REGISTER_AMD64_R13 = REGISTER_AMD64_R12 + 1,

        /// <summary>The #14 data register on the AMD64 processor.<summary>
        REGISTER_AMD64_R14 = REGISTER_AMD64_R13 + 1,

        /// <summary>The #15 data register on the AMD64 processor.<summary>
        REGISTER_AMD64_R15 = REGISTER_AMD64_R14 + 1,

        /// <summary>The #0 multimedia register on the AMD64 processor.<summary>
        REGISTER_AMD64_XMM0 = REGISTER_AMD64_R15 + 1,

        /// <summary>The #1 multimedia register on the AMD64 processor.<summary>
        REGISTER_AMD64_XMM1 = REGISTER_AMD64_XMM0 + 1,

        /// <summary>The #2 multimedia register on the AMD64 processor.<summary>
        REGISTER_AMD64_XMM2 = REGISTER_AMD64_XMM1 + 1,

        /// <summary>The #3 multimedia register on the AMD64 processor.<summary>
        REGISTER_AMD64_XMM3 = REGISTER_AMD64_XMM2 + 1,

        /// <summary>The #4 multimedia register on the AMD64 processor.<summary>
        REGISTER_AMD64_XMM4 = REGISTER_AMD64_XMM3 + 1,

        /// <summary>The #5 multimedia register on the AMD64 processor.<summary>
        REGISTER_AMD64_XMM5 = REGISTER_AMD64_XMM4 + 1,

        /// <summary>The #6 multimedia register on the AMD64 processor.<summary>
        REGISTER_AMD64_XMM6 = REGISTER_AMD64_XMM5 + 1,

        /// <summary>The #7 multimedia register on the AMD64 processor.<summary>
        REGISTER_AMD64_XMM7 = REGISTER_AMD64_XMM6 + 1,

        /// <summary>The #8 multimedia register on the AMD64 processor.<summary>
        REGISTER_AMD64_XMM8 = REGISTER_AMD64_XMM7 + 1,

        /// <summary>The #9 multimedia register on the AMD64 processor.<summary>
        REGISTER_AMD64_XMM9 = REGISTER_AMD64_XMM8 + 1,

        /// <summary>The #10 multimedia register on the AMD64 processor.<summary>
        REGISTER_AMD64_XMM10 = REGISTER_AMD64_XMM9 + 1,

        /// <summary>The #11 multimedia register on the AMD64 processor.<summary>
        REGISTER_AMD64_XMM11 = REGISTER_AMD64_XMM10 + 1,

        /// <summary>The #12 multimedia register on the AMD64 processor.<summary>
        REGISTER_AMD64_XMM12 = REGISTER_AMD64_XMM11 + 1,

        /// <summary>The #13 multimedia register on the AMD64 processor.<summary>
        REGISTER_AMD64_XMM13 = REGISTER_AMD64_XMM12 + 1,

        /// <summary>The #14 multimedia register on the AMD64 processor.<summary>
        REGISTER_AMD64_XMM14 = REGISTER_AMD64_XMM13 + 1,

        /// <summary>The #15 multimedia register on the AMD64 processor.<summary>
        REGISTER_AMD64_XMM15 = REGISTER_AMD64_XMM14 + 1,

        /// <summary>The stack pointer register on the IA-64 processor.<summary>
        REGISTER_IA64_BSP = REGISTER_FRAME_POINTER,

        /// <summary>The #0 data register on the IA-64 processor.<summary>
        REGISTER_IA64_R0 = REGISTER_IA64_BSP + 1,

        /// <summary>The #0 FP data register on the IA-64 processor.<summary>
        REGISTER_IA64_F0 = REGISTER_IA64_R0 + 1,

        /// <summary>The program counter register (R15) on the ARM processor.<summary>
        REGISTER_ARM_PC = 0,

        /// <summary>The stack pointer register (R13) on the ARM processor.<summary>
        REGISTER_ARM_SP = REGISTER_ARM_PC + 1,

        /// <summary>Data register R0 on the ARM processor.<summary>
        REGISTER_ARM_R0 = REGISTER_ARM_SP + 1,

        /// <summary>Data register R1 on the ARM processor.<summary>
        REGISTER_ARM_R1 = REGISTER_ARM_R0 + 1,

        /// <summary>Data register R2 on the ARM processor.<summary>
        REGISTER_ARM_R2 = REGISTER_ARM_R1 + 1,

        /// <summary>Data register R3 on the ARM processor.<summary>
        REGISTER_ARM_R3 = REGISTER_ARM_R2 + 1,

        /// <summary>Register R4 on the ARM processor.<summary>
        REGISTER_ARM_R4 = REGISTER_ARM_R3 + 1,

        /// <summary>Register R5 on the ARM processor.<summary>
        REGISTER_ARM_R5 = REGISTER_ARM_R4 + 1,

        /// <summary>Register R6 on the ARM processor.<summary>
        REGISTER_ARM_R6 = REGISTER_ARM_R5 + 1,

        /// <summary>Register R7 (the THUMB frame pointer) on the ARM processor.<summary>
        REGISTER_ARM_R7 = REGISTER_ARM_R6 + 1,

        /// <summary>Register R8 on the ARM processor.<summary>
        REGISTER_ARM_R8 = REGISTER_ARM_R7 + 1,

        /// <summary>Register R9 on the ARM processor.<summary>
        REGISTER_ARM_R9 = REGISTER_ARM_R8 + 1,

        /// <summary>Register R10 on the ARM processor.<summary>
        REGISTER_ARM_R10 = REGISTER_ARM_R9 + 1,

        /// <summary>The frame pointer on the ARM processor.<summary>
        REGISTER_ARM_R11 = REGISTER_ARM_R10 + 1,

        /// <summary>Register R12 on the ARM processor.<summary>
        REGISTER_ARM_R12 = REGISTER_ARM_R11 + 1,

        /// <summary>The link register (R14) on the ARM processor.<summary>
        REGISTER_ARM_LR = REGISTER_ARM_R12 + 1,

        REGISTER_ARM_D0 = REGISTER_ARM_LR + 1,

        REGISTER_ARM_D1 = REGISTER_ARM_D0 + 1,

        REGISTER_ARM_D2 = REGISTER_ARM_D1 + 1,

        REGISTER_ARM_D3 = REGISTER_ARM_D2 + 1,

        REGISTER_ARM_D4 = REGISTER_ARM_D3 + 1,

        REGISTER_ARM_D5 = REGISTER_ARM_D4 + 1,

        REGISTER_ARM_D6 = REGISTER_ARM_D5 + 1,

        REGISTER_ARM_D7 = REGISTER_ARM_D6 + 1,

        REGISTER_ARM_D8 = REGISTER_ARM_D7 + 1,

        REGISTER_ARM_D9 = REGISTER_ARM_D8 + 1,

        REGISTER_ARM_D10 = REGISTER_ARM_D9 + 1,

        REGISTER_ARM_D11 = REGISTER_ARM_D10 + 1,

        REGISTER_ARM_D12 = REGISTER_ARM_D11 + 1,

        REGISTER_ARM_D13 = REGISTER_ARM_D12 + 1,

        REGISTER_ARM_D14 = REGISTER_ARM_D13 + 1,

        REGISTER_ARM_D15 = REGISTER_ARM_D14 + 1,

        REGISTER_ARM_D16 = REGISTER_ARM_D15 + 1,

        REGISTER_ARM_D17 = REGISTER_ARM_D16 + 1,

        REGISTER_ARM_D18 = REGISTER_ARM_D17 + 1,

        REGISTER_ARM_D19 = REGISTER_ARM_D18 + 1,

        REGISTER_ARM_D20 = REGISTER_ARM_D19 + 1,

        REGISTER_ARM_D21 = REGISTER_ARM_D20 + 1,

        REGISTER_ARM_D22 = REGISTER_ARM_D21 + 1,

        REGISTER_ARM_D23 = REGISTER_ARM_D22 + 1,

        REGISTER_ARM_D24 = REGISTER_ARM_D23 + 1,

        REGISTER_ARM_D25 = REGISTER_ARM_D24 + 1,

        REGISTER_ARM_D26 = REGISTER_ARM_D25 + 1,

        REGISTER_ARM_D27 = REGISTER_ARM_D26 + 1,

        REGISTER_ARM_D28 = REGISTER_ARM_D27 + 1,

        REGISTER_ARM_D29 = REGISTER_ARM_D28 + 1,

        REGISTER_ARM_D30 = REGISTER_ARM_D29 + 1,

        REGISTER_ARM_D31 = REGISTER_ARM_D30 + 1,

        REGISTER_ARM64_PC = 0,

        REGISTER_ARM64_SP = REGISTER_ARM64_PC + 1,

        REGISTER_ARM64_FP = REGISTER_ARM64_SP + 1,

        REGISTER_ARM64_X0 = REGISTER_ARM64_FP + 1,

        REGISTER_ARM64_X1 = REGISTER_ARM64_X0 + 1,

        REGISTER_ARM64_X2 = REGISTER_ARM64_X1 + 1,

        REGISTER_ARM64_X3 = REGISTER_ARM64_X2 + 1,

        REGISTER_ARM64_X4 = REGISTER_ARM64_X3 + 1,

        REGISTER_ARM64_X5 = REGISTER_ARM64_X4 + 1,

        REGISTER_ARM64_X6 = REGISTER_ARM64_X5 + 1,

        REGISTER_ARM64_X7 = REGISTER_ARM64_X6 + 1,

        REGISTER_ARM64_X8 = REGISTER_ARM64_X7 + 1,

        REGISTER_ARM64_X9 = REGISTER_ARM64_X8 + 1,

        REGISTER_ARM64_X10 = REGISTER_ARM64_X9 + 1,

        REGISTER_ARM64_X11 = REGISTER_ARM64_X10 + 1,

        REGISTER_ARM64_X12 = REGISTER_ARM64_X11 + 1,

        REGISTER_ARM64_X13 = REGISTER_ARM64_X12 + 1,

        REGISTER_ARM64_X14 = REGISTER_ARM64_X13 + 1,

        REGISTER_ARM64_X15 = REGISTER_ARM64_X14 + 1,

        REGISTER_ARM64_X16 = REGISTER_ARM64_X15 + 1,

        REGISTER_ARM64_X17 = REGISTER_ARM64_X16 + 1,

        REGISTER_ARM64_X18 = REGISTER_ARM64_X17 + 1,

        REGISTER_ARM64_X19 = REGISTER_ARM64_X18 + 1,

        REGISTER_ARM64_X20 = REGISTER_ARM64_X19 + 1,

        REGISTER_ARM64_X21 = REGISTER_ARM64_X20 + 1,

        REGISTER_ARM64_X22 = REGISTER_ARM64_X21 + 1,

        REGISTER_ARM64_X23 = REGISTER_ARM64_X22 + 1,

        REGISTER_ARM64_X24 = REGISTER_ARM64_X23 + 1,

        REGISTER_ARM64_X25 = REGISTER_ARM64_X24 + 1,

        REGISTER_ARM64_X26 = REGISTER_ARM64_X25 + 1,

        REGISTER_ARM64_X27 = REGISTER_ARM64_X26 + 1,

        REGISTER_ARM64_X28 = REGISTER_ARM64_X27 + 1,

        REGISTER_ARM64_LR = REGISTER_ARM64_X28 + 1,

        REGISTER_ARM64_V0 = REGISTER_ARM64_LR + 1,

        REGISTER_ARM64_V1 = REGISTER_ARM64_V0 + 1,

        REGISTER_ARM64_V2 = REGISTER_ARM64_V1 + 1,

        REGISTER_ARM64_V3 = REGISTER_ARM64_V2 + 1,

        REGISTER_ARM64_V4 = REGISTER_ARM64_V3 + 1,

        REGISTER_ARM64_V5 = REGISTER_ARM64_V4 + 1,

        REGISTER_ARM64_V6 = REGISTER_ARM64_V5 + 1,

        REGISTER_ARM64_V7 = REGISTER_ARM64_V6 + 1,

        REGISTER_ARM64_V8 = REGISTER_ARM64_V7 + 1,

        REGISTER_ARM64_V9 = REGISTER_ARM64_V8 + 1,

        REGISTER_ARM64_V10 = REGISTER_ARM64_V9 + 1,

        REGISTER_ARM64_V11 = REGISTER_ARM64_V10 + 1,

        REGISTER_ARM64_V12 = REGISTER_ARM64_V11 + 1,

        REGISTER_ARM64_V13 = REGISTER_ARM64_V12 + 1,

        REGISTER_ARM64_V14 = REGISTER_ARM64_V13 + 1,

        REGISTER_ARM64_V15 = REGISTER_ARM64_V14 + 1,

        REGISTER_ARM64_V16 = REGISTER_ARM64_V15 + 1,

        REGISTER_ARM64_V17 = REGISTER_ARM64_V16 + 1,

        REGISTER_ARM64_V18 = REGISTER_ARM64_V17 + 1,

        REGISTER_ARM64_V19 = REGISTER_ARM64_V18 + 1,

        REGISTER_ARM64_V20 = REGISTER_ARM64_V19 + 1,

        REGISTER_ARM64_V21 = REGISTER_ARM64_V20 + 1,

        REGISTER_ARM64_V22 = REGISTER_ARM64_V21 + 1,

        REGISTER_ARM64_V23 = REGISTER_ARM64_V22 + 1,

        REGISTER_ARM64_V24 = REGISTER_ARM64_V23 + 1,

        REGISTER_ARM64_V25 = REGISTER_ARM64_V24 + 1,

        REGISTER_ARM64_V26 = REGISTER_ARM64_V25 + 1,

        REGISTER_ARM64_V27 = REGISTER_ARM64_V26 + 1,

        REGISTER_ARM64_V28 = REGISTER_ARM64_V27 + 1,

        REGISTER_ARM64_V29 = REGISTER_ARM64_V28 + 1,

        REGISTER_ARM64_V30 = REGISTER_ARM64_V29 + 1,

        REGISTER_ARM64_V31 = REGISTER_ARM64_V30 + 1,
    }
}
