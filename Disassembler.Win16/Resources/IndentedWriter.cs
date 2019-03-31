namespace Disassembler
{
    using System.IO;
    using System.Text;

    public class IndentedWriter : TextWriter
    {
        public TextWriter baseWriter;
        private bool startOfLine = true;

        public override Encoding Encoding => baseWriter.Encoding;
        public override void Close() => baseWriter.Close();
        public override string NewLine => baseWriter.NewLine;
        public override void Flush() => baseWriter.Flush();

        public IndentedWriter(TextWriter baseWriter)
        {
            this.baseWriter = baseWriter;
        }

        public int Indent { get; set; }

        public override void Write(char ch)
        {
            if (startOfLine)
            {
                startOfLine = false;
                for (int i = 0; i < Indent; i++)
                {
                    baseWriter.Write('\t');
                }
            }

            baseWriter.Write(ch);

            if (ch == '\n')
            {
                startOfLine = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (baseWriter != null) baseWriter.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
