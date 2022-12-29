using System.IO.Packaging;

const string X64_FILENAME = "syncthing-x64.exe";
const string X86_FILENAME = "syncthing-x86.exe";
const string PACKAGE_FILENAME = "syncthing-v1.22.2.zip";

/*
const string RELATIONSHIP_CORE_PROPERTIES = "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties";
const string RELATIONSHIP_DIGITAL_SIGNATURE = "http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/signature";
const string RELATIONSHIP_DIGITAL_SIGNATURE_CERTIFICATE = "http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/certificate";
const string RELATIONSHIP_DIGITAL_SIGNATURE_ORIGIN = "http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/origin";
const string RELATIONSHIP_THUMBNAIL = "http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail";
*/

// Convertimos las rutas del sistema, en rutas de un paquete
Uri partUrix64 = PackUriHelper.CreatePartUri(new Uri(X64_FILENAME, UriKind.Relative));
Uri partUrix86 = PackUriHelper.CreatePartUri(new Uri(X86_FILENAME, UriKind.Relative));

// Creamos el paquete
using (Package package = Package.Open(PACKAGE_FILENAME, FileMode.Create)) {
    package.PackageProperties.Version = "1.22.2";

    // Añadimos los archivos al paquete
    PackagePart packagePartx64 = package.CreatePart(partUrix64, System.Net.Mime.MediaTypeNames.Application.Octet, CompressionOption.Maximum);
    PackagePart packagePartx86 = package.CreatePart(partUrix86, System.Net.Mime.MediaTypeNames.Application.Octet, CompressionOption.Maximum);

    // Copiamos los datos al paquete
    using (FileStream fileStream = new FileStream(X64_FILENAME, FileMode.Open, FileAccess.Read)) {
        fileStream.CopyTo(packagePartx64.GetStream());
    }
    using (FileStream fileStream = new FileStream(X86_FILENAME, FileMode.Open, FileAccess.Read)) {
        fileStream.CopyTo(packagePartx86.GetStream());
    }

}
