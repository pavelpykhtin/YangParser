parser grammar YangRfcParser;

options { tokenVocab = YangRfcLexer; }
                                
moduleStmt                  : SEP? ModuleKeyword SEP identifier
                                 SEP?
                                 CURLYBRO stmtsep
                                     moduleHeaderStmts
                                     linkageStmts
                                     metaStmts
                                     revisionStmts
                                     bodyStmts
                                 CURLYBRC SEP? ;                                 
submoduleStmt               : SEP? SubmoduleKeyword SEP identifier
                                 SEP?
                                 CURLYBRO stmtsep
                                     submoduleHeaderStmts
                                     linkageStmts
                                     metaStmts
                                     revisionStmts
                                     bodyStmts
                                 CURLYBRC SEP? ;
moduleHeaderStmts           : // these stmts can appear in any order
                                 (yangVersionStmt |
                                 namespaceStmt    |
                                 prefixStmt)* ;
submoduleHeaderStmts        :
                                // these stmts can appear in any order
                                (yangVersionStmt |
                                belongsToStmt)* ;                         
metaStmts                  : // these stmts can appear in any order
                                (organizationStmt |
                                contactStmt |
                                descriptionStmt |
                                referenceStmt )* ;
linkageStmts                : // these stmts can appear in any order
                                (importStmt | includeStmt)* ;
revisionStmts               : revisionStmt* ;
bodyStmts                   : (extensionStmt |
                                   featureStmt |
                                   identityStmt |
                                   typedefStmt |
                                   groupingStmt |
                                   dataDefStmt |
                                   augmentStmt |
                                   rpcStmt |
                                   notificationStmt |
                                   deviationStmt)* ;
dataDefStmt                 : containerStmt |
                                 leafStmt |
                                 leafListStmt |
                                 listStmt |
                                 choiceStmt |
                                 anydataStmt |
                                 anyxmlStmt |
                                 usesStmt ;
yangVersionStmt             : YangVersionKeyword SEP yangVersionArgStr
                                stmtend ;
yangVersionArgStr           : YangVersion;
importStmt                  : ImportKeyword SEP identifier SEP?
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (prefixStmt |
                                     revisionDateStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                 CURLYBRC stmtsep ;                     
includeStmt                 : IncludeKeyword SEP identifier SEP?
                                 (SEMICOLON |
                                  CURLYBRO stmtsep
                                      // these stmts can appear in any order
                                      (revisionDateStmt |
                                      descriptionStmt |
                                      referenceStmt)*
                                  CURLYBRC) stmtsep ;
namespaceStmt               : NamespaceKeyword SEP uriStr stmtend ;
uriStr                      : quotedString;
prefixStmt                  : PrefixKeyword SEP prefixArgStr stmtend ;
belongsToStmt               : BelongsToKeyword SEP identifier
                                 SEP?
                                 CURLYBRO stmtsep
                                     prefixStmt
                                 CURLYBRC stmtsep ;
organizationStmt            : OrganizationKeyword SEP quotedString stmtend ;
contactStmt                 : ContactKeyword SEP quotedString stmtend ;
descriptionStmt             : DescriptionKeyword SEP quotedString stmtend ;
referenceStmt               : ReferenceKeyword SEP quotedString stmtend ;
unitsStmt                   : UnitsKeyword SEP quotedString stmtend ;
revisionStmt                : RevisionKeyword SEP revisionDate SEP?
                                 (SEMICOLON |
                                  CURLYBRO stmtsep
                                      // these stmts can appear in any order
                                      (descriptionStmt |
                                      referenceStmt)*
                                  CURLYBRC) stmtsep ;
revisionDate                : dateArgStr ;
revisionDateStmt            : RevisionDateKeyword SEP revisionDate stmtend ;
extensionStmt               : ExtensionKeyword SEP identifier SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (argumentStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt)* 
                                 CURLYBRC) stmtsep ;
argumentStmt                : ArgumentKeyword SEP identifier SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     yinElementStmt?
                                 CURLYBRC) stmtsep ;
yinElementStmt              : YinElementKeyword SEP yinElementArgStr
                                stmtend ;
yinElementArgStr            : yinElementArg ;
yinElementArg               : TrueKeyword | FalseKeyword ;
identityStmt                : IdentityKeyword SEP identifier SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (ifFeatureStmt |
                                     baseStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                 CURLYBRC) stmtsep ;

baseStmt                    : BaseKeyword SEP identifierRefArgStr
                                stmtend ;
featureStmt                 : FeatureKeyword SEP identifier SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (ifFeatureStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                 CURLYBRC) stmtsep ;
ifFeatureStmt               : IfFeatureKeyword SEP ifFeatureExprStr stmtend ;
ifFeatureExprStr            : quotedString ;
typedefStmt                 : TypedefKeyword SEP identifier SEP?
                                CURLYBRO stmtsep
                                    // these stmts can appear in any order
                                    (typeStmt |
                                    unitsStmt |
                                    defaultStmt |
                                    statusStmt |
                                    descriptionStmt |
                                    referenceStmt)*
                                 CURLYBRC stmtsep ;
typeStmt                    : TypeKeyword SEP identifierRefArgStr SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     typeBodyStmts?
                                 CURLYBRC) stmtsep ;
typeBodyStmts               : numericalRestrictions |
                                stringRestrictions |
                                enumSpecification |
                                leafrefSpecification |
                                identityrefSpecification |
                                instanceIdentifierSpecification |
                                bitsSpecification |
                                unionSpecification |
                                binarySpecification ;
numericalRestrictions       : // these stmts can appear in any order
                                  (fractionDigitsStmt |
                                  rangeStmt)* ;
rangeStmt                   : RangeKeyword SEP rangeArgStr SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (errorMessageStmt |
                                     errorAppTagStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                  CURLYBRC) stmtsep ;
fractionDigitsStmt          : FractionDigitsKeyword SEP
                                fractionDigitsArgStr stmtend ;
fractionDigitsArgStr        : integerValue ;
stringRestrictions          :
                                // these stmts can appear in any order
                                (lengthStmt |
                                patternStmt)* ;
lengthStmt                  : LengthKeyword SEP lengthArgStr SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (errorMessageStmt |
                                     errorAppTagStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                  CURLYBRC) stmtsep ;
patternStmt                 : PatternKeyword SEP patternArgStr SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (modifierStmt |
                                     errorMessageStmt |
                                     errorAppTagStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                  CURLYBRC) stmtsep ;
modifierStmt                : ModifierKeyword SEP modifierArgStr stmtend ;
modifierArgStr              : modifierArg ;
modifierArg                 : InvertMatchKeyword ;
defaultStmt                 : DefaultKeyword SEP quotedString stmtend ;
enumSpecification           : enumStmt+ ;
enumStmt                    : EnumKeyword SEP identifier SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (ifFeatureStmt |
                                     valueStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                  CURLYBRC) stmtsep ;

leafrefSpecification        :
                                // these stmts can appear in any order
                                (pathStmt |
                                requireInstanceStmt)* ;

pathStmt                    : PathKeyword SEP pathArgStr stmtend ;
requireInstanceStmt         : RequireInstanceKeyword SEP
                                requireInstanceArgStr stmtend ;
requireInstanceArgStr       : requireInstanceArg ;
requireInstanceArg          : TrueKeyword | FalseKeyword ; 
instanceIdentifierSpecification   : requireInstanceStmt? ;
identityrefSpecification    : baseStmt+ ;
unionSpecification          :  typeStmt+ ;
binarySpecification         : lengthStmt? ;
bitsSpecification           :  bitStmt+ ;
bitStmt                     : BitKeyword SEP identifier SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (ifFeatureStmt |
                                     positionStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                 CURLYBRC) stmtsep ;
positionStmt                : PositionKeyword SEP
                                positionValueArgStr stmtend ;
positionValueArgStr         : positionValueArg ; 
positionValueArg            : integerValue ;
statusStmt                  : StatusKeyword SEP statusArgStr stmtend ;
statusArgStr                : statusArg ;
statusArg                   : CurrentKeyword |
                                ObsoleteKeyword |
                                DeprecatedKeyword ;
configStmt                  : ConfigKeyword SEP
                                configArgStr stmtend ;
configArgStr                : configArg ;
configArg                   : TrueKeyword | FalseKeyword ;
mandatoryStmt               : MandatoryKeyword SEP 
                                mandatoryArgStr stmtend ;
mandatoryArgStr             : mandatoryArg ;
mandatoryArg                : TrueKeyword | FalseKeyword ;
presenceStmt                : PresenceKeyword SEP quotedString stmtend ;
orderedByStmt               : OrderedByKeyword SEP
                                orderedByArgStr stmtend ;
orderedByArgStr             : orderedByArg ;
orderedByArg                : UserKeyword | SystemKeyword ;
mustStmt                    : MustKeyword SEP quotedString SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (errorMessageStmt |
                                     errorAppTagStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                  CURLYBRC) stmtsep ;

errorMessageStmt            : ErrorMessageKeyword SEP quotedString stmtend;
errorAppTagStmt             : ErrorAppTagKeyword SEP quotedString stmtend;
minElementsStmt             : MinElementsKeyword SEP
                                minValueArgStr stmtend ;
minValueArgStr              : minValueArg ;
minValueArg                 : integerValue ;
maxElementsStmt             : MaxElementsKeyword SEP
                                maxValueArgStr stmtend;
maxValueArgStr              : maxValueArg ;
maxValueArg                 : UnboundedKeyword |
                                integerValue ;
valueStmt                   : ValueKeyword SEP integerValueStr stmtend ;
integerValueStr             : integerValue ;
groupingStmt                : GroupingKeyword SEP identifier SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (statusStmt |
                                     descriptionStmt |
                                     referenceStmt |
                                     (typedefStmt | groupingStmt) |
                                     dataDefStmt |
                                     actionStmt |
                                     notificationStmt)*
                                 CURLYBRC) stmtsep;
containerStmt               : ContainerKeyword SEP identifier SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (whenStmt |
                                     ifFeatureStmt |
                                     mustStmt |
                                     presenceStmt |
                                     configStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt |
                                     (typedefStmt | groupingStmt) |
                                     dataDefStmt |
                                     actionStmt |
                                     notificationStmt)*
                                 CURLYBRC) stmtsep ;

leafStmt                    : LeafKeyword SEP identifier SEP?
                                CURLYBRO stmtsep
                                    // these stmts can appear in any order
                                    (whenStmt |
                                    ifFeatureStmt |
                                    typeStmt |
                                    unitsStmt |
                                    mustStmt |
                                    defaultStmt |
                                    configStmt |
                                    mandatoryStmt |
                                    statusStmt |
                                    descriptionStmt |
                                    referenceStmt)*
                                 CURLYBRC stmtsep ;
leafListStmt                : LeafListKeyword SEP identifier SEP?
                                CURLYBRO stmtsep
                                    // these stmts can appear in any order
                                    (whenStmt |
                                    ifFeatureStmt |
                                    (typeStmt stmtsep) |
                                    unitsStmt |
                                    mustStmt |
                                    defaultStmt |
                                    configStmt |
                                    minElementsStmt |
                                    maxElementsStmt |
                                    orderedByStmt |
                                    statusStmt |
                                    descriptionStmt |
                                    referenceStmt)*
                                 CURLYBRC stmtsep;
listStmt                    : ListKeyword SEP identifier SEP?
                                CURLYBRO stmtsep
                                    // these stmts can appear in any order
                                    (whenStmt |
                                    ifFeatureStmt |
                                    mustStmt |
                                    keyStmt |
                                    uniqueStmt |
                                    configStmt |
                                    minElementsStmt |
                                    maxElementsStmt |
                                    orderedByStmt |
                                    statusStmt |
                                    descriptionStmt |
                                    referenceStmt |
                                    (typedefStmt | groupingStmt) |
                                    dataDefStmt |
                                    actionStmt |
                                    notificationStmt)*
                                 CURLYBRC stmtsep ;
keyStmt                     : KeyKeyword SEP keyArgStr stmtend ;
keyArgStr                   : keyArg ;
keyArg                      : nodeIdentifier (SEP nodeIdentifier)* ;
uniqueStmt                  : UniqueKeyword SEP uniqueArgStr stmtend ;
uniqueArgStr                : quotedString ;
choiceStmt                  : ChoiceKeyword SEP identifier SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (whenStmt |
                                     ifFeatureStmt |
                                     defaultStmt |
                                     configStmt |
                                     mandatoryStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt |
                                     shortCaseStmt | 
                                     caseStmt)*
                                 CURLYBRC) stmtsep ;
shortCaseStmt               : choiceStmt |
                                containerStmt |
                                leafStmt |
                                leafListStmt |
                                listStmt |
                                anydataStmt |
                                anyxmlStmt ;
caseStmt                    : CaseKeyword SEP identifier SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (whenStmt |
                                     ifFeatureStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt |
                                     dataDefStmt)*
                                 CURLYBRC) stmtsep ;
anydataStmt                 : AnydataKeyword SEP identifier SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (whenStmt |
                                     ifFeatureStmt |
                                     mustStmt |
                                     configStmt |
                                     mandatoryStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                  CURLYBRC) stmtsep ;
anyxmlStmt                  : AnyxmlKeyword SEP identifier SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (whenStmt |
                                     ifFeatureStmt |
                                     mustStmt |
                                     configStmt |
                                     mandatoryStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                  CURLYBRC) stmtsep ;
usesStmt                    : UsesKeyword SEP identifierRefArgStr SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (whenStmt
                                     ifFeatureStmt
                                     statusStmt
                                     descriptionStmt
                                     referenceStmt
                                     refineStmt
                                     usesAugmentStmt)*
                                 CURLYBRC) stmtsep ;
refineStmt                  : RefineKeyword SEP refineArgStr SEP?
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (ifFeatureStmt |
                                     mustStmt |
                                     presenceStmt |
                                     defaultStmt |
                                     configStmt |
                                     mandatoryStmt |
                                     minElementsStmt |
                                     maxElementsStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                   CURLYBRC stmtsep ;
refineArgStr                : quotedString ;
usesAugmentStmt             : AugmentKeyword SEP usesAugmentArgStr SEP?
                                CURLYBRO stmtsep
                                    // these stmts can appear in any order
                                    (whenStmt |
                                    ifFeatureStmt |
                                    statusStmt |
                                    descriptionStmt |
                                    referenceStmt |
                                    (dataDefStmt | caseStmt |
                                       actionStmt | notificationStmt)+)*
                                 CURLYBRC stmtsep ;
usesAugmentArgStr           : usesAugmentArg ;
usesAugmentArg              : descendantSchemaNodeid;
augmentStmt                 : AugmentKeyword SEP augmentArgStr SEP?
                                CURLYBRO stmtsep
                                    // these stmts can appear in any order
                                    (whenStmt |
                                    ifFeatureStmt |
                                    statusStmt |
                                    descriptionStmt |
                                    referenceStmt |
                                    (dataDefStmt | caseStmt |
                                       actionStmt | notificationStmt)+)*
                                 CURLYBRC stmtsep ;
augmentArgStr               : augmentArg ;
augmentArg                  : absoluteSchemaNodeid ;
whenStmt                    : WhenKeyword SEP quotedString SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (descriptionStmt |
                                     referenceStmt)*
                                  CURLYBRC) stmtsep ;
rpcStmt                     : RpcKeyword SEP identifier SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (ifFeatureStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt |
                                     (typedefStmt | groupingStmt) |
                                     inputStmt |
                                     outputStmt)*
                                 CURLYBRC) stmtsep ;
actionStmt                  : ActionKeyword SEP identifier SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (ifFeatureStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt |
                                     (typedefStmt | groupingStmt) |
                                     inputStmt |
                                     outputStmt)* 
                                 CURLYBRC) stmtsep ;
inputStmt                   : InputKeyword SEP?
                                CURLYBRO stmtsep
                                    // these stmts can appear in any order
                                    (mustStmt |
                                    (typedefStmt | groupingStmt) |
                                    dataDefStmt)*
                                CURLYBRC stmtsep ;
outputStmt                  : OutputKeyword SEP?
                                CURLYBRO stmtsep
                                    // these stmts can appear in any order
                                    (mustStmt |
                                    (typedefStmt | groupingStmt) |
                                    dataDefStmt)*
                                CURLYBRC stmtsep ;
notificationStmt            : NotificationKeyword SEP
                                identifier SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (ifFeatureStmt |
                                     mustStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt |
                                     (typedefStmt | groupingStmt) |
                                     dataDefStmt)*
                                 CURLYBRC) stmtsep ;
deviationStmt               : DeviationKeyword SEP
                                deviationArgStr SEP?
                                CURLYBRO stmtsep
                                    // these stmts can appear in any order
                                    (descriptionStmt |
                                    referenceStmt |
                                    deviateNotSupportedStmt |
                                    deviateAddStmt |
                                    deviateReplaceStmt |
                                    deviateDeleteStmt)*
                                CURLYBRC stmtsep ;
deviationArgStr             : deviationArg ;
deviationArg                : absoluteSchemaNodeid ;
deviateNotSupportedStmt     :   DeviateKeyword SEP
                                    notSupportedKeywordStr stmtend ;
deviateAddStmt              : DeviateKeyword SEP addKeywordStr SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (unitsStmt |
                                     mustStmt |
                                     uniqueStmt |
                                     defaultStmt |
                                     configStmt |
                                     mandatoryStmt |
                                     minElementsStmt |
                                     maxElementsStmt)*
                                 CURLYBRC) stmtsep ;
deviateDeleteStmt           : DeviateKeyword SEP deleteKeywordStr SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (unitsStmt |
                                     mustStmt |
                                     uniqueStmt |
                                     defaultStmt)*
                                 CURLYBRC) stmtsep ;
deviateReplaceStmt          : DeviateKeyword SEP replaceKeywordStr SEP?
                                (SEMICOLON |
                                 CURLYBRO stmtsep
                                     // these stmts can appear in any order
                                     (typeStmt |
                                     unitsStmt |
                                     defaultStmt |
                                     configStmt |
                                     mandatoryStmt |
                                     minElementsStmt |
                                     maxElementsStmt)*
                                 CURLYBRC) stmtsep ;
notSupportedKeywordStr      : NotSupportedKeyword ;
addKeywordStr               : AddKeyword ;
deleteKeywordStr            : DeleteKeyword ;
replaceKeywordStr           : ReplaceKeyword ;

// represents the usage of an extension
unknownStatement            : prefix COLON identifier (SEP string)? SEP?
                                (SEMICOLON |
                                    CURLYBRO SEP?
                                    ((yangStmt | unknownStatement) SEP?)*
                                    CURLYBRC) stmtsep ;

yangStmt                    : actionStmt |
                                anydataStmt |
                                anyxmlStmt |
                                argumentStmt |
                                augmentStmt |
                                baseStmt |
                                belongsToStmt |
                                bitStmt |
                                caseStmt |
                                choiceStmt |
                                configStmt |
                                contactStmt |
                                containerStmt |
                                defaultStmt |
                                descriptionStmt |
                                deviateAddStmt |
                                deviateDeleteStmt |
                                deviateNotSupportedStmt |
                                deviateReplaceStmt |
                                deviationStmt |
                                enumStmt |
                                errorAppTagStmt |
                                errorMessageStmt |
                                extensionStmt |
                                featureStmt |
                                fractionDigitsStmt |
                                groupingStmt |
                                identityStmt |
                                ifFeatureStmt |
                                importStmt |
                                includeStmt |
                                inputStmt |
                                keyStmt |
                                leafListStmt |
                                leafStmt |
                                lengthStmt |
                                listStmt |
                                mandatoryStmt |
                                maxElementsStmt |
                                minElementsStmt |
                                modifierStmt |
                                moduleStmt |
                                mustStmt |
                                namespaceStmt |
                                notificationStmt |
                                orderedByStmt |
                                organizationStmt |
                                outputStmt |
                                pathStmt |
                                patternStmt |
                                positionStmt |
                                prefixStmt |
                                presenceStmt |
                                rangeStmt |
                                referenceStmt |
                                refineStmt |
                                requireInstanceStmt |
                                revisionDateStmt |
                                revisionStmt |
                                rpcStmt |
                                statusStmt |
                                submoduleStmt |
                                typedefStmt |
                                typeStmt |
                                uniqueStmt |
                                unitsStmt |
                                usesAugmentStmt |
                                usesStmt |
                                valueStmt |
                                whenStmt |
                                yangVersionStmt |
                                yinElementStmt ;

// ranges
rangeArgStr                 : quotedString ;

// lengths
lengthArgStr                : quotedString ;

// pattern
patternArgStr               : quotedString ;

// date
dateArgStr                  : DIGIT DIGIT DIGIT DIGIT 
                                DASH 
                                DIGIT DIGIT
                                DASH
                                DIGIT DIGIT;

// schema node identifiers
schemaNodeid                : absoluteSchemaNodeid |
                                descendantSchemaNodeid ;
absoluteSchemaNodeid        : (SLASH nodeIdentifier)+ ;
descendantSchemaNodeid      : nodeIdentifier
                                (absoluteSchemaNodeid)? ;
nodeIdentifier              : (prefix COLON)? identifier ;

// instance identifiers
instanceIdentifier          : (SLASH (nodeIdentifier
                                (   keyPredicate+ |
                                    leafListPredicate |
                                    pos)?))+ ;
keyPredicate                : SQRBRO WSP* keyPredicateExpr WSP* SQRBRC ;
keyPredicateExpr            : nodeIdentifier WSP* EQ WSP* quotedString ;
leafListPredicate           : SQRBRO WSP* leafListPredicateExpr WSP* SQRBRC ;
leafListPredicateExpr       : DOT WSP* EQ WSP* quotedString ;
pos                         : SQRBRO WSP* integerValue WSP* SQRBRC ;
quotedString                : (DQUOTE dquoteContent DQUOTE) | (SQUOTE squoteContent SQUOTE) ;

// leafref path
pathArgStr                  : quotedString ;

// keywords, using the syntax for caseSensitive strings (RFC 7405)

currentFunctionInvocation   : CurrentKeyword WSP* BRO WSP* BRC ;

// basic rules
prefixArgStr                : prefixArg | quotedString ;
prefixArg                   : prefix ;
prefix                      : identifier ;

identifierRefArgStr         : quotedString | identifierRefArg ;
identifierRefArg            : identifierRef ;
identifierRef               : ( prefix COLON )? identifier ;
                    
string                      : yangString ;
                    
yangString                  : yangChar* ;
squoteContent               : ~SQUOTE*;
dquoteContent               : ~DQUOTE*;

// any unicode or ISO/IEC 10646 character, including tab, carriage
// return, and line feed but excluding the other C0 control
// characters, the surrogate blocks, and the noncharacters
yangChar                    : WSP | LF | CR | EXTRA_CHAR;

integerValue                : DIGIT+ ;

stmtend             : SEP? ( SEMICOLON | CURLYBRO stmtsep CURLYBRC ) stmtsep ;
stmtsep             : (SEP | unknownStatement)* ; //(WSP | LINEBREAK | unknownStatement)* ;
decimalValue        : integerValue (DOT integerValue) ;

identifier          : ID |
                        ActionKeyword |
                        AnydataKeyword |
                        AnyxmlKeyword |
                        ArgumentKeyword |
                        AugmentKeyword |
                        BaseKeyword |
                        BelongsToKeyword |
                        BitKeyword |
                        CaseKeyword |
                        ChoiceKeyword |
                        ConfigKeyword |
                        ContactKeyword |
                        ContainerKeyword |
                        DefaultKeyword |
                        DescriptionKeyword |
                        DeviateKeyword |
                        DeviationKeyword |
                        EnumKeyword |
                        ErrorAppTagKeyword |
                        ErrorMessageKeyword |
                        ExtensionKeyword |
                        FeatureKeyword |
                        FractionDigitsKeyword |
                        GroupingKeyword |
                        IdentityKeyword |
                        IfFeatureKeyword |
                        ImportKeyword |
                        IncludeKeyword |
                        InputKeyword |
                        KeyKeyword |
                        LeafKeyword |
                        LeafListKeyword |
                        LengthKeyword |
                        ListKeyword |
                        MandatoryKeyword |
                        MaxElementsKeyword |
                        MinElementsKeyword |
                        ModifierKeyword |
                        ModuleKeyword |
                        MustKeyword |
                        NamespaceKeyword |
                        NotificationKeyword |
                        OrderedByKeyword |
                        OrganizationKeyword |
                        OutputKeyword |
                        PathKeyword |
                        PatternKeyword |
                        PositionKeyword |
                        PrefixKeyword |
                        PresenceKeyword |
                        RangeKeyword |
                        ReferenceKeyword |
                        RefineKeyword |
                        RequireInstanceKeyword |
                        RevisionKeyword |
                        RevisionDateKeyword |
                        RpcKeyword |
                        StatusKeyword |
                        SubmoduleKeyword |
                        TypeKeyword |
                        TypedefKeyword |
                        UniqueKeyword |
                        UnitsKeyword |
                        UsesKeyword |
                        ValueKeyword |
                        WhenKeyword |
                        YangVersionKeyword |
                        YinElementKeyword |
                        AddKeyword |
                        CurrentKeyword |
                        DeleteKeyword |
                        DeprecatedKeyword |
                        FalseKeyword |
                        InvertMatchKeyword |
                        MaxKeyword |
                        MinKeyword |
                        NotSupportedKeyword |
                        ObsoleteKeyword |
                        ReplaceKeyword |
                        SystemKeyword |
                        TrueKeyword |
                        UnboundedKeyword |
                        UserKeyword |
                        AndKeyword |
                        OrKeyword |
                        NotKeyword;