using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpMath.Editor {
  public enum MathKeyboardInput {
    //Navigation
    Up = '⏶', Down = '⏷', Left = '⏴', Right = '⏵',
    Backspace = '⌫', Clear = '⎚', Return = '\n', Dismiss = '❌',

    //Brackets
    LeftRoundBracket = '(', RightRoundBracket = ')', LeftSquareBracket = '[', RightSquareBracket = ']',
    LeftCurlyBracket = '{', RightCurlyBracket = '}',

    //Decimals
    D0 = '0', D1 = '1', D2 = '2', D3 = '3', D4 = '4',
    D5 = '5', D6 = '6', D7 = '7', D8 = '8', D9 = '9', Decimal = '.',

    //Basic operators
    Plus = '+', Minus = '−', Minus_ = '-', Multiply = '×', Multiply_ = '*', Divide = '÷', Ratio = '∶', 
    Ratio_ = ':', Percentage = '%', Comma = ',', Factorial = '!', Infinity = '∞', Angle = '∠', Degree = '°',
    VerticalBar = '|', Logarithm = '㏒', NaturalLogarithm = '㏑',

    //More complicated operators
    BothRoundBrackets = '㈾', Slash = '/', Fraction = '⁄', Power = '^', Subscript = '_', SquareRoot = '√', 
    CubeRoot = '∛', NthRoot = '∜', Absolute = '‖', BaseEPower = 'ℯ', LogarithmWithBase = '㏐',

    //Relations
    Equals = '=', NotEquals = '≠',
    LessThan = '<', LessOrEquals = '≤', GreaterThan = '>', GreaterOrEquals = '≥',

    //Capital English alphabets
    A = 'A', B = 'B', C = 'C', D = 'D', E = 'E', F = 'F', G = 'G', H = 'H', I = 'I',
    J = 'J', K = 'K', L = 'L', M = 'M', N = 'N', O = 'O', P = 'P', Q = 'Q', R = 'R',
    S = 'S', T = 'T', U = 'U', V = 'V', W = 'W', X = 'X', Y = 'Y', Z = 'Z',

    //Small English alphabets
    SmallA = 'a', SmallB = 'b', SmallC = 'c', SmallD = 'd', SmallE = 'e',
    SmallF = 'f', SmallG = 'g', SmallH = 'h', SmallI = 'i', SmallJ = 'j',
    SmallK = 'k', SmallL = 'l', SmallM = 'm', SmallN = 'n', SmallO = 'o',
    SmallP = 'p', SmallQ = 'q', SmallR = 'r', SmallS = 's', SmallT = 't',
    SmallU = 'u', SmallV = 'v', SmallW = 'w', SmallX = 'x', SmallY = 'y', SmallZ = 'z',

    //Capital Greek alphabets
    Alpha = 'Α', Beta = 'Β', Gamma = 'Γ', Delta = 'Δ', Epsilon = 'Ε', Zeta = 'Ζ', Eta = 'Η', 
    Theta = 'Θ', Iota = 'Ι', Kappa = 'Κ', Lambda = 'Λ', Mu = 'Μ', Nu = 'Ν', Xi = 'Ξ', Omicron = 'Ο', 
    Pi = 'Π', Rho = 'Ρ', Sigma = 'Σ', Tau = 'Τ', Psi = 'Ψ', Upsilon = 'Υ', Phi = 'Φ', Chi = 'Χ', Omega = 'Ω',

    //Small Greek alphabets
    SmallAlpha = 'α', SmallBeta = 'β', SmallGamma = 'γ', SmallDelta = 'δ', SmallEpsilon = 'ε',
    SmallZeta = 'ζ', SmallEta = 'η', SmallTheta = 'θ', SmallIota = 'ι', SmallKappa = 'κ',
    SmallLambda = 'λ', SmallMu = 'μ', SmallNu = 'ν', SmallXi = 'ξ', SmallOmicron = 'ο',
    SmallPi = 'π', SmallRho = 'ρ', SmallSigma = 'σ', SmallSigma2 = 'ς', SmallTau = 'τ', SmallPsi = 'ψ',
    SmallUpsilon = 'υ', SmallPhi = 'φ', SmallChi = 'χ', SmallOmega = 'ω',

    Partial = '∂', Jmath = 0x0001D6A5, IMath = 0x0001D6A4, Triangle = 0x25B3, DDots = 0x22F1, CDots = 0x22EF, VDots = 0x22EE, Bot = 0x22A5,
    Top = 0x22A4, Nabla = 0x2207, EmptySet = 0x2205, ThereFore = 0x2234, Because = 0x2235, Exists = 0x2203, ForAll = 0x2200, Daleth = 0x2138,
    Gimel = 0x2137, Beth = 0x2136, Aleph = 0x2135, Mho = 0x2127, Re = 0x211C, Wp = 0x2118, Ell = 0x2113, Im = 0x2111, Hbar = 0x210F, IDots = 0x2026,


    //Trigonometric functions
    Sine = '␖', Cosine = '℅', Tangent = '␘', Cotangent = '␄', Secant = '␎', Cosecant = '␛', ArcSine = '◜', 
    ArcCosine = '◝', ArcTangent = '◟', ArcCotangent = '◞', ArcSecant = '◠', ArcCosecant = '◡',
    
    //Hyperbolic functions
    HyperbolicSine = '◐', HyperbolicCosine = '◑', HyperbolicTangent = '◓',
    HyperbolicCotangent = '◒', HyperbolicSecant = '◔', HyperbolicCosecant = '◕',
    AreaHyperbolicSine = '◴', AreaHyperbolicCosine = '◷', AreaHyperbolicTangent = '◵',
    AreaHyperbolicCotangent = '◶', AreaHyperbolicSecant = '⚆', AreaHyperbolicCosecant = '⚇',
    Integral = '∫', IntegralSub= 0x22f2, IntegralSup = 0x22f3, IntegralSubSup = 0x22f4, Sum = '∑', SumSub= 0x22f5, SumSup= 0x22f6, SumSubSup= 0x22f7,
    DoubleIntegral = '∬', TripleIntegral = '∭', ContourIntegral = '∮', DoubleContourIntegral = '∯', TripleContourIntegral = '∰', ClockwiseIntegral = '∱',
    ClockwiseContourIntegral = '∲', CounterClockwiseContourIntegral = '∳'

  }
}
