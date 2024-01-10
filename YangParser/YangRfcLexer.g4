lexer grammar YangRfcLexer;


YangVersion                 : '1.1';

// statement keywords
ActionKeyword               : 'action';
AnydataKeyword              : 'anydata';
AnyxmlKeyword               : 'anyxml';
ArgumentKeyword             : 'argument';
AugmentKeyword              : 'augment';
BaseKeyword                 : 'base';
BelongsToKeyword            : 'belongs-to';
BitKeyword                  : 'bit';
CaseKeyword                 : 'case';
ChoiceKeyword               : 'choice';
ConfigKeyword               : 'config';
ContactKeyword              : 'contact';
ContainerKeyword            : 'container';
DefaultKeyword              : 'default';
DescriptionKeyword          : 'description';
DeviateKeyword              : 'deviate';
DeviationKeyword            : 'deviation';
EnumKeyword                 : 'enum';
ErrorAppTagKeyword          : 'error-app-tag';
ErrorMessageKeyword         : 'error-message';
ExtensionKeyword            : 'extension';
FeatureKeyword              : 'feature';
FractionDigitsKeyword       : 'fraction-digits';
GroupingKeyword             : 'grouping';
IdentityKeyword             : 'identity';
IfFeatureKeyword            : 'if-feature';
ImportKeyword               : 'import';
IncludeKeyword              : 'include';
InputKeyword                : 'input';
KeyKeyword                  : 'key';
LeafKeyword                 : 'leaf';
LeafListKeyword             : 'leaf-list';
LengthKeyword               : 'length';
ListKeyword                 : 'list';
MandatoryKeyword            : 'mandatory';
MaxElementsKeyword          : 'max-elements';
MinElementsKeyword          : 'min-elements';
ModifierKeyword             : 'modifier';
ModuleKeyword               : 'module';
MustKeyword                 : 'must';
NamespaceKeyword            : 'namespace';
NotificationKeyword         : 'notification';
OrderedByKeyword            : 'ordered-by';
OrganizationKeyword         : 'organization';
OutputKeyword               : 'output';
PathKeyword                 : 'path';
PatternKeyword              : 'pattern';
PositionKeyword             : 'position';
PrefixKeyword               : 'prefix';
PresenceKeyword             : 'presence';
RangeKeyword                : 'range';
ReferenceKeyword            : 'reference';
RefineKeyword               : 'refine';
RequireInstanceKeyword      : 'require-instance';
RevisionKeyword             : 'revision';
RevisionDateKeyword         : 'revision-date';
RpcKeyword                  : 'rpc';
StatusKeyword               : 'status';
SubmoduleKeyword            : 'submodule';
TypeKeyword                 : 'type';
TypedefKeyword              : 'typedef';
UniqueKeyword               : 'unique';
UnitsKeyword                : 'units';
UsesKeyword                 : 'uses';
ValueKeyword                : 'value';
WhenKeyword                 : 'when';
YangVersionKeyword          : 'yang-version';
YinElementKeyword           : 'yin-element';

ID                          : (ALPHA | UNDERSCORE)
                              (ALPHA | DIGIT | UNDERSCORE | DASH | DOT)*;
                              
// other keywords
AddKeyword                  : 'add' ;
CurrentKeyword              : 'current' ;
DeleteKeyword               : 'delete' ;
DeprecatedKeyword           : 'deprecated' ;
FalseKeyword                : 'false' ;
InvertMatchKeyword          : 'invertMatch' ;
MaxKeyword                  : 'max' ;
MinKeyword                  : 'min' ;
NotSupportedKeyword         : 'notSupported' ;
ObsoleteKeyword             : 'obsolete' ;
ReplaceKeyword              : 'replace' ;
SystemKeyword               : 'system' ;
TrueKeyword                 : 'true' ;
UnboundedKeyword            : 'unbounded' ;
UserKeyword                 : 'user' ;
AndKeyword                  : 'and';
OrKeyword                   : 'or';
NotKeyword                  : 'not';

// core rules from RFC 5234
SEP                 : (WSP | LINEBREAK)+ ;                                // unconditional separator
SQUOTE              : '\'' ;
ALPHA               : [A-Za-z] ;
LINEBREAK           : CRLF | LF ;
CRLF                : CR LF ;                                               // internet standard newline
CR                  : '\r' ;                                                // carriage return
LF                  : '\n' ;                                                // line feed
DIGIT               : [0-9] ;
DASH                : '-' ;
UNDERSCORE          : '_' ;
DQUOTE              : '"' ;
CURLYBRO            : '{';
CURLYBRC            : '}';
BRO                 : '(';
BRC                 : ')';
SQRBRO              : '[';
SQRBRC              : ']';
SEMICOLON           : ';' ;
COLON               : ':' ;
SLASH               : '/';
EQ                  : '=';
DOT                  : '.';

HTAB                : '\t' ;                                                // horizontal tab
SP                  : ' ' ;                                                 // space
WSP                 : (' ' | '\t');                                         // whitespace
EXTRA_CHAR           : 
                        [\u0020-\uD7FF]   |                         // exclude surrogate blocks %xD800DFFF
                        [\uE000-\uFDCF]   |                         // exclude noncharacters %xFDD0FDEF
                        [\uFDF0-\uFFFD] ;                           // exclude noncharacters %xFFFEFFFF
//                        [\u10000-\u1FFFD] |                       // exclude noncharacters %x1FFFE-1FFFF
//                        [\u20000-\u2FFFD] |                       // exclude noncharacters %x2FFFE-2FFFF
//                        [\u30000-\u3FFFD] |                       // exclude noncharacters %x3FFFE-3FFFF
//                        [\u40000-\u4FFFD] |                       // exclude noncharacters %x4FFFE-4FFFF
//                        [\u50000-\u5FFFD] |                       // exclude noncharacters %x5FFFE-5FFFF
//                        [\u60000-\u6FFFD] |                       // exclude noncharacters %x6FFFE-6FFFF
//                        [\u70000-\u7FFFD] |                       // exclude noncharacters %x7FFFE-7FFFF
//                        [\u80000-\u8FFFD] |                       // exclude noncharacters %x8FFFE-8FFFF
//                        [\u90000-\u9FFFD] |                       // exclude noncharacters %x9FFFE-9FFFF
//                        [\uA0000-\uAFFFD] |                       // exclude noncharacters %xAFFFEAFFFF
//                        [\uB0000-\uBFFFD] |                       // exclude noncharacters %xBFFFEBFFFF
//                        [\uC0000-\uCFFFD] |                       // exclude noncharacters %xCFFFECFFFF
//                        [\uD0000-\uDFFFD] |                       // exclude noncharacters %xDFFFEDFFFF
//                        [\uE0000-\uEFFFD] |                       // exclude noncharacters %xEFFFEEFFFF
//                        [\uF0000-\uFFFFD] |                       // exclude noncharacters %xFFFFEFFFFF
//                        [\u100000-\u10FFFD] ;                     // exclude noncharacters %x10FFFE-10FFFF
