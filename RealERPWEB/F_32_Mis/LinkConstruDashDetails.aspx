<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkConstruDashDetails.aspx.cs" Inherits="RealERPWEB.F_32_Mis.LinkConstruDashDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

    </script>
    <style>
        .grvHeader th{
            font-weight:normal;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:Label ID="lbljavascript" runat="server"></asp:Label>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">

                        <%--    <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-8 pading5px">
                                       <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:Label ID="lblvalDate" runat="server" CssClass="inputTxt inpPixedWidth" Width="100px" TabIndex="10"></asp:Label>
                                      
                                        <asp:Label ID="lblproject" runat="server" CssClass="lblTxt lblName">Project Name:</asp:Label>
                                        <asp:Label ID="lblvalproject" runat="server" CssClass=" inputlblVal" Style="width:250px;"></asp:Label>

                                        <asp:Label ID="lbltk" runat="server" CssClass="lblTxt lblName" Style="font-size:16px;">Taka in Lac </asp:Label>
                                    </div>
                                  
                                    


                                </div>
                               
                                </div>--%>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <div class="col-md-7">
                            <div class="row">
                            <asp:GridView ID="gvConPro" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" ShowFooter="True" Width="378px" PageSize="20"
                            OnPageIndexChanging="gvConPro_PageIndexChanging">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                

                                <asp:TemplateField HeaderText="Project Name ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvWorkPrj" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFWorkPrj" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="50px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                
                                <asp:TemplateField HeaderText="Items">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvWorkitems" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFWorkitems" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="50px" Text="Total :"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Budget">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvWorkbgd" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFWorkBdg" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="50px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderText="Execution">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvWorkexe" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "execution")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFWorkexe" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="50px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderText="Work % ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvWorkPer" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "parcent")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFWorkPer" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                
                                <asp:TemplateField HeaderText="Rest Work % ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvWorkRest" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "restwrk")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFWorkRest" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                  
                                <asp:TemplateField HeaderText="Start Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvstartdate" runat="server" Style="text-align: left"
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy")) %>'                                                                                  
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFstartdate" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                  
                                <asp:TemplateField HeaderText="End Date ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvEndate" runat="server" Style="text-align: left"
                                          Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddate")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddate")).ToString("dd-MMM-yyyy")) %>'                                          
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFenddate" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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
              
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

