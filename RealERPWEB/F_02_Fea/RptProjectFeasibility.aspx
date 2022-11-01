<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptProjectFeasibility.aspx.cs" Inherits="RealERPWEB.F_02_Fea.RptProjectFeasibility" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">


       $(document).ready(function () {

           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

       });

       function pageLoaded() {

           $("input, select").bind("keydown", function (event) {
               var k1 = new KeyPress();
               k1.textBoxHandler(event);
           });
           $('.chzn-select').chosen({ search_contains: true });
           
          
        }


     

    </script>
    <style type="text/css" >
        .table th, .table td{
            padding: 2px;
        }
    </style>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="card mt-4">
                <div class="card-body">
                    <div class="row mb-4">

                                <div class="d-none">
                                    <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth d-none" Width="80px"></asp:TextBox>
                                    <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn d-none" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label4" runat="server" CssClass="from-label" Text="Project Name:"></asp:Label>

                                    <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>


                                </div>
                                <div class="col-md-2 ml-3" style="margin-top:22px;">
                                    <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lnkbtnSerOk_Click" CssClass="btn btn-primary btn-sm">Ok</asp:LinkButton>
                                </div>
                            


                        


                    </div>
                </div>
            </div>
            <div class="card" style="min-height:480px;">
                <div class="card-body">
                    <div class="row"> 
                          <div class="table table-responsive">
                <asp:GridView ID="gvFeaPrjRep" runat="server" BackColor="White" AutoGenerateColumns="False" CssClass=" table-striped table-bordered grvContentarea"
                    ShowFooter="True" Width="785px" OnRowDataBound="gvFeaPrjRep_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Sl #">
                            <ItemTemplate>
                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" 
                                    Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblinfocde" runat="server" AutoCompleteType="Disabled" Height="5px"
                                    BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                    Width="50px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Group Description">
                            <ItemTemplate>
                                <asp:Label ID="lgvgroupdesc"  runat="server" AutoCompleteType="Disabled"
                                    BackColor="Transparent" BorderStyle="None" Font-Size="10px"
                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subgrpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                        
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "subgrpdesc")).Trim()+"</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "infdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?   "<br>" 
                                                                          :(Convert.ToString(DataBinder.Eval(Container.DataItem, "subgrpdesc")).Trim().Length>0 ? "<br>":"")) + 
                                                                 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")).Trim(): "")
                                                                    %>'
                                    Width="180px"></asp:Label>

                            </ItemTemplate>
                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description of Item" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lgvItemdescRep" runat="server" AutoCompleteType="Disabled"
                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc2")) %>'
                                    Width="150px"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lgUnitnumRep" runat="server" AutoCompleteType="Disabled"
                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                    Width="50px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Size">
                            <ItemTemplate>
                                <asp:Label ID="lgSizerep" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="11px"  Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="50px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Number">
                            <ItemTemplate>
                                <asp:Label ID="lgnumrep" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="11px"  Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "number")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="50px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total SFT / Quantity">

                            <ItemTemplate>
                                <asp:Label ID="lgtsizecRep" runat="server" Font-Size="11px"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsize")).ToString("#,##0;(#,##0); ") %>'
                                    Width="100px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate/SFT">

                            <ItemTemplate>
                                <asp:Label ID="lgsalraterep" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="100px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="lgvAmtrep" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                    Width="100px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFAmtc0" runat="server" Font-Bold="True" Font-Size="12px"
                                    ForeColor="White" Style="text-align: right"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Percentage %" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lgvper" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pecent")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="right" />
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
          
            </div>





            <%--<tr>
                                    <td>&nbsp;</td>
                                    <td class="style58">
                                        <asp:Label ID="Label4" runat="server" CssClass="style50" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: right" Text="Project Name:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style57">
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ibtnFindProject_Click" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True"
                                            Font-Size="12px" Width="400px">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366"
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" OnClick="lnkbtnSerOk_Click"
                                            Style="color: #FFFFFF; height: 17px;">Ok</asp:LinkButton>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>--%>
                          


                        


                  
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

