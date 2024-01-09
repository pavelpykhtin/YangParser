grammar YangRfc;
                                
moduleStmt                  : SEP? moduleKeyword SEP identifierArgStr
                                 SEP?
                                 '{' stmtsep
                                     moduleHeaderStmts
                                     linkageStmts
                                     metaStmts
                                     revisionStmts
                                     bodyStmts
                                 '}' SEP? ;                                 
submoduleStmt               : SEP? submoduleKeyword SEP identifierArgStr
                                 SEP?
                                 '{' stmtsep
                                     submoduleHeaderStmts
                                     linkageStmts
                                     metaStmts
                                     revisionStmts
                                     bodyStmts
                                 '}' SEP? ;
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
yangVersionStmt             : yangVersionKeyword SEP yangVersionArgStr
                                stmtend ;
yangVersionArgStr           : yangVersionArg;
yangVersionArg              : '1.1' ;
importStmt                  : importKeyword SEP identifierArgStr SEP?
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (prefixStmt |
                                     revisionDateStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                 '}' stmtsep ;                     
includeStmt                 : includeKeyword SEP identifierArgStr SEP?
                                 (';' |
                                  '{' stmtsep
                                      // these stmts can appear in any order
                                      (revisionDateStmt |
                                      descriptionStmt |
                                      referenceStmt)*
                                  '}') stmtsep ;
namespaceStmt               : namespaceKeyword SEP uriStr stmtend ;
uriStr                      : quotedString;
prefixStmt                  : prefixKeyword SEP prefixArgStr stmtend ;
belongsToStmt               : belongsToKeyword SEP identifierArgStr
                                 SEP?
                                 '{' stmtsep
                                     prefixStmt
                                 '}' stmtsep ;
organizationStmt            : organizationKeyword SEP string stmtend ;
contactStmt                 : contactKeyword SEP string stmtend ;
descriptionStmt             : descriptionKeyword SEP quotedString stmtend ;
referenceStmt               : referenceKeyword SEP quotedString stmtend ;
unitsStmt                   : unitsKeyword SEP string stmtend ;
revisionStmt                : revisionKeyword SEP revisionDate SEP?
                                 (';' |
                                  '{' stmtsep
                                      // these stmts can appear in any order
                                      (descriptionStmt |
                                      referenceStmt)*
                                  '}') stmtsep ;
revisionDate                : dateArgStr ;
revisionDateStmt            : revisionDateKeyword SEP revisionDate stmtend ;
extensionStmt               : extensionKeyword SEP identifierArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (argumentStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt)* 
                                 '}') stmtsep ;
argumentStmt                : argumentKeyword SEP identifierArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     yinElementStmt?
                                 '}') stmtsep ;
yinElementStmt              : yinElementKeyword SEP yinElementArgStr
                                stmtend ;
yinElementArgStr            : yinElementArg ;
yinElementArg               : trueKeyword | falseKeyword ;
identityStmt                : identityKeyword SEP identifierArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (ifFeatureStmt |
                                     baseStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                 '}') stmtsep ;

baseStmt                    : baseKeyword SEP identifierRefArgStr
                                stmtend ;
featureStmt                 : featureKeyword SEP identifierArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (ifFeatureStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                 '}') stmtsep ;
ifFeatureStmt               : ifFeatureKeyword SEP ifFeatureExprStr stmtend ;
ifFeatureExprStr            : quotedString ;
typedefStmt                 : typedefKeyword SEP identifierArgStr SEP?
                                '{' stmtsep
                                    // these stmts can appear in any order
                                    (typeStmt |
                                    unitsStmt |
                                    defaultStmt |
                                    statusStmt |
                                    descriptionStmt |
                                    referenceStmt)*
                                 '}' stmtsep ;
typeStmt                    : typeKeyword SEP identifierRefArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     typeBodyStmts?
                                 '}') stmtsep ;
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
rangeStmt                   : rangeKeyword SEP rangeArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (errorMessageStmt |
                                     errorAppTagStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                  '}') stmtsep ;
fractionDigitsStmt          : fractionDigitsKeyword SEP
                                fractionDigitsArgStr stmtend ;
fractionDigitsArgStr        : integerValue ;
stringRestrictions          :
                                // these stmts can appear in any order
                                (lengthStmt |
                                patternStmt)* ;
lengthStmt                  : lengthKeyword SEP lengthArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (errorMessageStmt |
                                     errorAppTagStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                  '}') stmtsep ;
patternStmt                 : patternKeyword SEP patternArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (modifierStmt |
                                     errorMessageStmt |
                                     errorAppTagStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                  '}') stmtsep ;
modifierStmt                : modifierKeyword SEP modifierArgStr stmtend ;
modifierArgStr              : modifierArg ;
modifierArg                 : invertMatchKeyword ;
defaultStmt                 : defaultKeyword SEP string stmtend ;
enumSpecification           : enumStmt+ ;
enumStmt                    : enumKeyword SEP identifier SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (ifFeatureStmt |
                                     valueStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                  '}') stmtsep ;

leafrefSpecification        :
                                // these stmts can appear in any order
                                (pathStmt |
                                requireInstanceStmt)* ;

pathStmt                    : pathKeyword SEP pathArgStr stmtend ;
requireInstanceStmt         : requireInstanceKeyword SEP
                                requireInstanceArgStr stmtend ;
requireInstanceArgStr       : requireInstanceArg ;
requireInstanceArg          : trueKeyword | falseKeyword ; 
instanceIdentifierSpecification   : requireInstanceStmt? ;
identityrefSpecification    : baseStmt+ ;
unionSpecification          :  typeStmt+ ;
binarySpecification         : lengthStmt? ;
bitsSpecification           :  bitStmt+ ;
bitStmt                     : bitKeyword SEP identifierArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (ifFeatureStmt |
                                     positionStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                 '}') stmtsep ;
positionStmt                : positionKeyword SEP
                                positionValueArgStr stmtend ;
positionValueArgStr         : positionValueArg ; 
positionValueArg            : integerValue ;
statusStmt                  : statusKeyword SEP statusArgStr stmtend ;
statusArgStr                : statusArg ;
statusArg                   : currentKeyword |
                                obsoleteKeyword |
                                deprecatedKeyword ;
configStmt                  : configKeyword SEP
                                configArgStr stmtend ;
configArgStr                : configArg ;
configArg                   : trueKeyword | falseKeyword ;
mandatoryStmt               : mandatoryKeyword SEP 
                                mandatoryArgStr stmtend ;
mandatoryArgStr             : mandatoryArg ;
mandatoryArg                : trueKeyword | falseKeyword ;
presenceStmt                : presenceKeyword SEP string stmtend ;
orderedByStmt               : orderedByKeyword SEP
                                orderedByArgStr stmtend ;
orderedByArgStr             : orderedByArg ;
orderedByArg                : userKeyword | systemKeyword ;
mustStmt                    : mustKeyword SEP string SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (errorMessageStmt |
                                     errorAppTagStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                  '}') stmtsep ;

errorMessageStmt            : errorMessageKeyword SEP quotedString stmtend;
errorAppTagStmt             : errorAppTagKeyword SEP quotedString stmtend;
minElementsStmt             : minElementsKeyword SEP
                                minValueArgStr stmtend ;
minValueArgStr              : minValueArg ;
minValueArg                 : integerValue ;
maxElementsStmt             : maxElementsKeyword SEP
                                maxValueArgStr stmtend;
maxValueArgStr              : maxValueArg ;
maxValueArg                 : unboundedKeyword |
                                integerValue ;
valueStmt                   : valueKeyword SEP integerValueStr stmtend ;
integerValueStr             : integerValue ;
groupingStmt                : groupingKeyword SEP identifierArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (statusStmt |
                                     descriptionStmt |
                                     referenceStmt |
                                     (typedefStmt | groupingStmt) |
                                     dataDefStmt |
                                     actionStmt |
                                     notificationStmt)
                                 '}') stmtsep;
containerStmt               : containerKeyword SEP identifierArgStr SEP?
                                (';' |
                                 '{' stmtsep
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
                                 '}') stmtsep ;

leafStmt                    : leafKeyword SEP identifierArgStr SEP?
                                '{' stmtsep
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
                                 '}' stmtsep ;
leafListStmt                : leafListKeyword SEP identifierArgStr SEP?
                                '{' stmtsep
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
                                 '}' stmtsep;
listStmt                    : listKeyword SEP identifierArgStr SEP?
                                '{' stmtsep
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
                                 '}' stmtsep ;
keyStmt                     : keyKeyword SEP keyArgStr stmtend ;
keyArgStr                   : keyArg ;
keyArg                      : nodeIdentifier (SEP nodeIdentifier)* ;
uniqueStmt                  : uniqueKeyword SEP uniqueArgStr stmtend ;
uniqueArgStr                : uniqueArg ;
uniqueArg                   : descendantSchemaNodeid
                                (SEP descendantSchemaNodeid)* ;
choiceStmt                  : choiceKeyword SEP identifierArgStr SEP?
                                (';' |
                                 '{' stmtsep
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
                                 '}') stmtsep ;
shortCaseStmt               : choiceStmt |
                                containerStmt |
                                leafStmt |
                                leafListStmt |
                                listStmt |
                                anydataStmt |
                                anyxmlStmt ;
caseStmt                    : caseKeyword SEP identifierArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (whenStmt |
                                     ifFeatureStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt |
                                     dataDefStmt)*
                                 '}') stmtsep ;
anydataStmt                 : anydataKeyword SEP identifierArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (whenStmt |
                                     ifFeatureStmt |
                                     mustStmt |
                                     configStmt |
                                     mandatoryStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                  '}') stmtsep ;
anyxmlStmt                  : anyxmlKeyword SEP identifierArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (whenStmt |
                                     ifFeatureStmt |
                                     mustStmt |
                                     configStmt |
                                     mandatoryStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt)*
                                  '}') stmtsep ;
usesStmt                    : usesKeyword SEP identifierRefArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (whenStmt
                                     ifFeatureStmt
                                     statusStmt
                                     descriptionStmt
                                     referenceStmt
                                     refineStmt
                                     usesAugmentStmt)*
                                 '}') stmtsep ;
refineStmt                  : refineKeyword SEP refineArgStr SEP?
                                 '{' stmtsep
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
                                   '}' stmtsep ;
refineArgStr                : refineArg ;
refineArg                   : descendantSchemaNodeid ;
usesAugmentStmt             : augmentKeyword SEP usesAugmentArgStr SEP?
                                '{' stmtsep
                                    // these stmts can appear in any order
                                    (whenStmt |
                                    ifFeatureStmt |
                                    statusStmt |
                                    descriptionStmt |
                                    referenceStmt |
                                    (dataDefStmt | caseStmt |
                                       actionStmt | notificationStmt)+)*
                                 '}' stmtsep ;
usesAugmentArgStr           : usesAugmentArg ;
usesAugmentArg              : descendantSchemaNodeid;
augmentStmt                 : augmentKeyword SEP augmentArgStr SEP?
                                '{' stmtsep
                                    // these stmts can appear in any order
                                    (whenStmt |
                                    ifFeatureStmt |
                                    statusStmt |
                                    descriptionStmt |
                                    referenceStmt |
                                    (dataDefStmt | caseStmt |
                                       actionStmt | notificationStmt)+)*
                                 '}' stmtsep ;
augmentArgStr               : augmentArg ;
augmentArg                  : absoluteSchemaNodeid ;
whenStmt                    : whenKeyword SEP string SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (descriptionStmt |
                                     referenceStmt)*
                                  '}') stmtsep ;
rpcStmt                     : rpcKeyword SEP identifierArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (ifFeatureStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt |
                                     (typedefStmt | groupingStmt) |
                                     inputStmt |
                                     outputStmt)*
                                 '}') stmtsep ;
actionStmt                  : actionKeyword SEP identifierArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (ifFeatureStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt |
                                     (typedefStmt | groupingStmt) |
                                     inputStmt |
                                     outputStmt)* 
                                 '}') stmtsep ;
inputStmt                   : inputKeyword SEP?
                                '{' stmtsep
                                    // these stmts can appear in any order
                                    (mustStmt |
                                    (typedefStmt | groupingStmt) |
                                    dataDefStmt)*
                                '}' stmtsep ;
outputStmt                  : outputKeyword SEP?
                                '{' stmtsep
                                    // these stmts can appear in any order
                                    (mustStmt |
                                    (typedefStmt | groupingStmt) |
                                    dataDefStmt)*
                                '}' stmtsep ;
notificationStmt            : notificationKeyword SEP
                                identifierArgStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (ifFeatureStmt |
                                     mustStmt |
                                     statusStmt |
                                     descriptionStmt |
                                     referenceStmt |
                                     (typedefStmt | groupingStmt) |
                                     dataDefStmt)*
                                 '}') stmtsep ;
deviationStmt               : deviationKeyword SEP
                                deviationArgStr SEP?
                                '{' stmtsep
                                    // these stmts can appear in any order
                                    (descriptionStmt |
                                    referenceStmt |
                                    deviateNotSupportedStmt |
                                    deviateAddStmt |
                                    deviateReplaceStmt |
                                    deviateDeleteStmt)*
                                '}' stmtsep ;
deviationArgStr             : deviationArg ;
deviationArg                : absoluteSchemaNodeid ;
deviateNotSupportedStmt     :   deviateKeyword SEP
                                    notSupportedKeywordStr stmtend ;
deviateAddStmt              : deviateKeyword SEP addKeywordStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (unitsStmt |
                                     mustStmt |
                                     uniqueStmt |
                                     defaultStmt |
                                     configStmt |
                                     mandatoryStmt |
                                     minElementsStmt |
                                     maxElementsStmt)*
                                 '}') stmtsep ;
deviateDeleteStmt           : deviateKeyword SEP deleteKeywordStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (unitsStmt |
                                     mustStmt |
                                     uniqueStmt |
                                     defaultStmt)*
                                 '}') stmtsep ;
deviateReplaceStmt          : deviateKeyword SEP replaceKeywordStr SEP?
                                (';' |
                                 '{' stmtsep
                                     // these stmts can appear in any order
                                     (typeStmt |
                                     unitsStmt |
                                     defaultStmt |
                                     configStmt |
                                     mandatoryStmt |
                                     minElementsStmt |
                                     maxElementsStmt)*
                                 '}') stmtsep ;
notSupportedKeywordStr      : notSupportedKeyword ;
addKeywordStr               : addKeyword ;
deleteKeywordStr            : deleteKeyword ;
replaceKeywordStr           : replaceKeyword ;

// represents the usage of an extension
unknownStatement            : prefix ':' identifier (SEP string)? SEP?
                                (';' |
                                    '{' SEP?
                                    ((yangStmt | unknownStatement) SEP?)*
                                    '}') stmtsep ;

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
dateArgStr                  : quotedString ;

// schema node identifiers
schemaNodeid                : absoluteSchemaNodeid |
                                descendantSchemaNodeid ;
absoluteSchemaNodeid        : ('/' nodeIdentifier)+ ;
descendantSchemaNodeid      : nodeIdentifier
                                (absoluteSchemaNodeid)? ;
nodeIdentifier              : (prefix ':')? identifier ;

// instance identifiers
instanceIdentifier          : ('/' (nodeIdentifier
                                (   keyPredicate+ |
                                    leafListPredicate |
                                    pos)?))+ ;
keyPredicate                : '[' WSP* keyPredicateExpr WSP* ']' ;
keyPredicateExpr            : nodeIdentifier WSP* '=' WSP* quotedString ;
leafListPredicate           : '[' WSP* leafListPredicateExpr WSP* ']' ;
leafListPredicateExpr       : '.' WSP* '=' WSP* quotedString ;
pos                         : '[' WSP* integerValue WSP* ']' ;
quotedString                : (DQUOTE dquoteContent DQUOTE) | (SQUOTE squoteContent SQUOTE) ;

// leafref path
pathArgStr                  : quotedString ;

// keywords, using the syntax for caseSensitive strings (RFC 7405)

// statement keywords
actionKeyword               : 'action';
anydataKeyword              : 'anydata';
anyxmlKeyword               : 'anyxml';
argumentKeyword             : 'argument';
augmentKeyword              : 'augment';
baseKeyword                 : 'base';
belongsToKeyword            : 'belongs-to';
bitKeyword                  : 'bit';
caseKeyword                 : 'case';
choiceKeyword               : 'choice';
configKeyword               : 'config';
contactKeyword              : 'contact';
containerKeyword            : 'container';
defaultKeyword              : 'default';
descriptionKeyword          : 'description';
deviateKeyword              : 'deviate';
deviationKeyword            : 'deviation';
enumKeyword                 : 'enum';
errorAppTagKeyword          : 'error-app-tag';
errorMessageKeyword         : 'error-message';
extensionKeyword            : 'extension';
featureKeyword              : 'feature';
fractionDigitsKeyword       : 'fraction-digits';
groupingKeyword             : 'grouping';
identityKeyword             : 'identity';
ifFeatureKeyword            : 'if-feature';
importKeyword               : 'import';
includeKeyword              : 'include';
inputKeyword                : 'input';
keyKeyword                  : 'key';
leafKeyword                 : 'leaf';
leafListKeyword             : 'leaf-list';
lengthKeyword               : 'length';
listKeyword                 : 'list';
mandatoryKeyword            : 'mandatory';
maxElementsKeyword          : 'max-elements';
minElementsKeyword          : 'min-elements';
modifierKeyword             : 'modifier';
moduleKeyword               : 'module';
mustKeyword                 : 'must';
namespaceKeyword            : 'namespace';
notificationKeyword         : 'notification';
orderedByKeyword            : 'ordered-by';
organizationKeyword         : 'organization';
outputKeyword               : 'output';
pathKeyword                 : 'path';
patternKeyword              : 'pattern';
positionKeyword             : 'position';
prefixKeyword               : 'prefix';
presenceKeyword             : 'presence';
rangeKeyword                : 'range';
referenceKeyword            : 'reference';
refineKeyword               : 'refine';
requireInstanceKeyword      : 'require-instance';
revisionKeyword             : 'revision';
revisionDateKeyword         : 'revision-date';
rpcKeyword                  : 'rpc';
statusKeyword               : 'status';
submoduleKeyword            : 'submodule';
typeKeyword                 : 'type';
typedefKeyword              : 'typedef';
uniqueKeyword               : 'unique';
unitsKeyword                : 'units';
usesKeyword                 : 'uses';
valueKeyword                : 'value';
whenKeyword                 : 'when';
yangVersionKeyword          : 'yang-version';
yinElementKeyword           : 'yin-element';

// other keywords
addKeyword                  : 'add' ;
currentKeyword              : 'current' ;
deleteKeyword               : 'delete' ;
deprecatedKeyword           : 'deprecated' ;
falseKeyword                : 'false' ;
invertMatchKeyword          : 'invertMatch' ;
maxKeyword                  : 'max' ;
minKeyword                  : 'min' ;
notSupportedKeyword         : 'notSupported' ;
obsoleteKeyword             : 'obsolete' ;
replaceKeyword              : 'replace' ;
systemKeyword               : 'system' ;
trueKeyword                 : 'true' ;
unboundedKeyword            : 'unbounded' ;
userKeyword                 : 'user' ;
andKeyword                  : 'and';
orKeyword                   : 'or';
notKeyword                  : 'not';

currentFunctionInvocation   : currentKeyword WSP* '(' WSP* ')' ;

// basic rules
prefixArgStr                : prefixArg ;
prefixArg                   : prefix ;
prefix                      : identifier ;
identifierArgStr            : identifierArg ;
identifierArg               : identifier ;
identifier                  : (ALPHA | '_')
                                (ALPHA | DIGIT | '_' | '-' | '.')* ;

identifierRefArgStr         : quotedString | identifierRefArg ;
identifierRefArg            : identifierRef ;
identifierRef               : ( prefix ':' )? identifier ;
                    
string                      : yangString ;
                    
yangString                  : yangChar* ;
squoteContent               : ~SQUOTE*;
dquoteContent               : ~DQUOTE*;

// any unicode or ISO/IEC 10646 character, including tab, carriage
// return, and line feed but excluding the other C0 control
// characters, the surrogate blocks, and the noncharacters
yangChar                    : WSP | LF | CR | EXTRA_CHAR;

integerValue                : DIGIT+ ;

stmtend             : SEP? ( ';' | '{' stmtsep '}' ) stmtsep ;
stmtsep             : (SEP | unknownStatement)* ; //(WSP | LINEBREAK | unknownStatement)* ;
decimalValue        : integerValue ('.' integerValue) ;

// core rules from RFC 5234
SEP                 : (WSP | LINEBREAK)+ ;                                // unconditional separator
SQUOTE              : '\'' ;
ALPHA               : [A-Za-z] ;
LINEBREAK           : CRLF | LF ;
CRLF                : CR LF ;                                               // internet standard newline
CR                  : '\r' ;                                                // carriage return
LF                  : '\n' ;                                                // line feed
DIGIT               : [0-9] ;
DQUOTE              : '"' ;
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