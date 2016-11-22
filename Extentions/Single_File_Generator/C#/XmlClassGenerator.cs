/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System;
using System.Runtime.InteropServices;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Microsoft.VisualStudio.Shell;
using VSLangProj80;

namespace Microsoft.Samples.VisualStudio.GeneratorSample
{
    /// <summary>
    /// This is the generator class. 
    /// When setting the 'Custom Tool' property of a C#, VB, or J# project item to "XmlClassGenerator", 
    /// the GenerateCode function will get called and will return the contents of the generated file 
    /// to the project system
    /// </summary>
    [ComVisible(true)]
    [Guid("52B316AA-1997-4c81-9969-83604C09EEB4")]
    [CodeGeneratorRegistration(typeof(XmlClassGenerator),"C# XML Class Generator",vsContextGuids.vsContextGuidVCSProject,GeneratesDesignTimeSource=true)]
    [CodeGeneratorRegistration(typeof(XmlClassGenerator), "VB XML Class Generator", vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(XmlClassGenerator), "J# XML Class Generator", vsContextGuids.vsContextGuidVJSProject, GeneratesDesignTimeSource = true)]
    [ProvideObject(typeof(XmlClassGenerator))]
    public class XmlClassGenerator : BaseCodeGeneratorWithSite
    {
#pragma warning disable 0414
        //The name of this generator (use for 'Custom Tool' property of project item)
        internal static string Name = "XmlClassGenerator";
#pragma warning restore 0414

        internal static bool ValidXml;

        /// <summary>
        /// Function that builds the contents of the generated file based on the contents of the input file
        /// </summary>
        /// <param name="inputFileContent">Content of the input file</param>
        /// <returns>Generated file as a byte array</returns>
        protected override byte[] GenerateCode(string inputFileContent)
        {
            //Validate the XML file against the schema
            using (StringReader contentReader = new StringReader(inputFileContent))
            {
                VerifyDocumentSchema(contentReader);
            }

            if (!ValidXml)
            {
                //Returning null signifies that generation has failed
                return null;
            }

            CodeDomProvider provider = GetCodeProvider();
            
            try
            {
                //Load the XML file
                XmlDocument doc = new XmlDocument();
                //We have already validated the XML. No XmlException can be thrown here
                doc.LoadXml(inputFileContent);

                //Create the CodeCompileUnit from the passed-in XML file
                CodeCompileUnit compileUnit = SourceCodeGenerator.CreateCodeCompileUnit(doc, FileNameSpace);

                //Report that we are 1/2 done
                CodeGeneratorProgress?.Progress(50, 100);

                using (StringWriter writer = new StringWriter(new StringBuilder()))
                {
                    CodeGeneratorOptions options = new CodeGeneratorOptions
                    {
                        BlankLinesBetweenMembers = false,
                        BracingStyle = "C"
                    };

                    //Generate the code
                    provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);

                    //Report that we are done
                    CodeGeneratorProgress?.Progress(100, 100);
                    writer.Flush();

                    //Get the Encoding used by the writer. We're getting the WindowsCodePage encoding, 
                    //which may not work with all languages
                    Encoding enc = Encoding.GetEncoding(writer.Encoding.WindowsCodePage);
                    
                    //Get the preamble (byte-order mark) for our encoding
                    byte[] preamble = enc.GetPreamble();
                    int preambleLength = preamble.Length;
                    
                    //Convert the writer contents to a byte array
                    byte[] body = enc.GetBytes(writer.ToString());

                    //Prepend the preamble to body (store result in resized preamble array)
                    Array.Resize(ref preamble, preambleLength + body.Length);
                    Array.Copy(body, 0, preamble, preambleLength, body.Length);
                    
                    //Return the combined byte array
                    return preamble;                    
                }
            }
            catch (Exception e)
            {
                GeneratorError(4, e.ToString(), 1, 1);
                //Returning null signifies that generation has failed
                return null;
            }
        }

        /// <summary>
        /// Verify the XML document in contentReader against the schema in XMLClassGeneratorSchema.xsd
        /// </summary>
        /// <param name="contentReader">TextReader for XML document</param>
        private void VerifyDocumentSchema(TextReader contentReader)
        {
            // Options for XmlReader object can be set only in constructor. After the object is created, 
            // they become read-only. Because of that we need to create XmlSettings structure, 
            // fill it in with correct parameters and pass into XmlReader constructor.
            XmlReaderSettings validatorSettings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                XmlResolver = null
            };
            validatorSettings.ValidationEventHandler += OnSchemaValidationError;

            //Schema is embedded in this assembly. Get its stream
            Stream schema = GetType().Assembly.GetManifestResourceStream("Microsoft.Samples.VisualStudio.GeneratorSample.XmlClassGeneratorSchema.xsd");

            if (schema != null)
                using (XmlTextReader schemaReader = new XmlTextReader(schema))
                {
                    try
                    {
                        validatorSettings.Schemas.Add("http://microsoft.com/XMLClassGeneratorSchema.xsd", schemaReader);
                    }
                    // handle errors in the schema itself
                    catch (XmlException e)
                    {
                        GeneratorError(4, "InvalidSchemaFileEmbeddedInGenerator " + e, 1, 1);
                        ValidXml = false;
                        return;
                    }
                    // handle errors in the schema itself
                    catch (XmlSchemaException e)
                    {
                        GeneratorError(4, "InvalidSchemaFileEmbeddedInGenerator " + e, 1, 1);
                        ValidXml = false;
                        return;
                    }

                    using (XmlReader validator = XmlReader.Create(contentReader, validatorSettings, InputFilePath))
                    {
                        ValidXml = true;
                        try
                        {
                            while (validator.Read())
                            {
                            }
                        }
                        catch (XmlException e)
                        {
                            GeneratorError(4, "InvalidContentEmbeddedInGenerator " + e, 1, 1);
                            ValidXml = false;
                        }
                    }
                }
        }

        /// <summary>
        /// Receives any errors that occur while validating the documents's schema.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">Details about the validation error that has occurred</param>
        private void OnSchemaValidationError(object sender, ValidationEventArgs args)
        {
            //signal that validation of document against schema has failed
            ValidXml = false;

            //Report the error (so that it is shown in the error list)
            GeneratorError(4, args.Exception.Message, (uint)args.Exception.LineNumber - 1, (uint)args.Exception.LinePosition - 1);
        }
    }
}