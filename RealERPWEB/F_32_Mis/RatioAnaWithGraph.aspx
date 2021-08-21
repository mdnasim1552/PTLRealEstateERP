<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RatioAnaWithGraph.aspx.cs" Inherits="RealERPWEB.F_32_Mis.RatioAnaWithGraph" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
     <script src="<%=this.ResolveUrl("~/Scripts/highchartwithmap.js")%>"></script>
    <script src="../Scripts/exporting.js"></script>
    <script src="../Scripts/export-data.js"></script>


    <script type="text/javascript" >

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

        }



        function ExecuteGraph(bgd) {

           
            var alldata = bgd;

            var bgddata = JSON.parse(bgd);
   
            var armainhead = [];
            for (var i = 0; i < bgddata.length; i++) {
                armainhead[i] = bgddata[i]["rdesc"];
            }

            var stddata = [];
            
            for (var i = 0; i < bgddata.length; i++) {
                stddata[i] = bgddata[i]["rstd"];
            }
            var actdata = [];
            for (var i = 0; i < bgddata.length; i++) {
                actdata[i] = bgddata[i]["ratio"];
            }
            console.log(armainhead);


            Highcharts.chart('ratioana', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Ratio Analysis (' + bgddata[0]["grpdesc"] + ')'
                },
                xAxis: {
                    categories: armainhead
                },
                credits: {
                    enabled: false
                },
                series: [{
                    name: 'Actual Ratio',
                    data: actdata 
                }, {
                    name: 'Standard Ratio',
                    data: stddata
                }]
            });





            


        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    

                    <div class="row">
                        <div class="col-md-6">
                            <asp:LinkButton runat="server" ID="ratiohead" CssClass="btn btn-success" style="margin-top:15px;"></asp:LinkButton>
                            <asp:GridView ID="gvIncomeMon" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="500px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlpsum" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                


                                <asp:TemplateField HeaderText=" Particulers">
                                    <ItemTemplate>

                                        <asp:Label ID="lblgvPart" runat="server"
                                            Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rdesc"))  %>'
                                            Width="120px" Style="font-size: 11px; color: Black;"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Formula">
                                    <ItemTemplate>

                                        <asp:Label ID="Label1" runat="server"
                                            Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rfourm"))  %>'
                                            Width="170px" Style="font-size: 11px; color: Black;"></asp:Label>


                                        <asp:Label ID="lblgvPart" runat="server"
                                            Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "formuladec1"))  %>'
                                            Width="170px" Style="font-size: 11px; color: Black;"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Ratio">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtoamtmpaysum" runat="server" Style="text-align: right" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ratio"))%>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoamtmpaysum" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Standard">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFamtmpaysum1" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvamtmpaysum1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rstd")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="%" Visible="false">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvt" runat="server" Style="text-align: right" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ratioprcen"))%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Interpretaion">
                                    <ItemTemplate>

                                        <asp:Label ID="lblgvPart" runat="server"
                                            Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inter"))  %>'
                                            Width="60px" Style="font-size: 11px"></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                        </div>
                        <div class="col-md-6">
                             <div id="ratioana" style="width: 500px; height: 300px; margin: 0 auto"></div>
                        </div>
                        
                    </div>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

