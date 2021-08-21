<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAllProTopSheetHL.aspx.cs" Inherits="RealERPWEB.F_01_LPA.RptAllProTopSheetHL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol4">

                                        <asp:Label ID="prname" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>

                                        <asp:TextBox ID="txtDate" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to" Text="To"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                                        <%--<asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnSerOk_Click">Ok</asp:LinkButton>--%>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to" Text="Category :"></asp:Label>
                                        <asp:TextBox ID="lblcat" runat="server" CssClass="form-control" Width="200px" AutoPostBack="true" TabIndex="2"></asp:TextBox>                                  
                                       <%-- <asp:DropDownList ID="ddlcatag"  Visible="False"  runat="server" CssClass="chzn-select form-control" Width="200px" AutoPostBack="true" TabIndex="2"></asp:DropDownList>--%><%-- <asp:DropDownList ID="ddlcatag"  Visible="False"  runat="server" CssClass="chzn-select form-control" Width="200px" AutoPostBack="true" TabIndex="2"></asp:DropDownList>--%>
                                        <%--<asp:HyperLink ID="HyperLink1" Target="_blank" NavigateUrl="~/F_17_Acc/AccSubCodeBook.aspx?InputType=Wrkschedule" runat="server" CssClass="btn btn-sm btn-success pad2px"> <span class="glyphicon glyphicon-pencil"></span> Add New Work</asp:HyperLink>--%>
                                    </div>
                                </div>
                            </div>
                        </fieldset>

                        <div class="table-responsive">

                            <asp:GridView ID="gvFeaPrjLand" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                Width="785px" OnRowDataBound="gvFeaPrjLand_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvproname" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))   %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Offered Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcdate" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdate"))   %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Land Owner's Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlownername" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lwnname"))   %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Land Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvladdress" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "laddress"))   %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvllocation" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "location"))   %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlcategory" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "category"))   %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TOTAL LAND <br/> AREA IN KHATA">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvltotallanare" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totallanare")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Land Size (Katha)" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlsize" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lsize"))   %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Storied" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvstoried" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "storied"))   %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Duration (Month) " Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvduration" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "duration"))   %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Developer Share(%)" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvdevshare" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "devshare"))   %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cash Benefit Offered">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlwamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lwnamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFlwamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cash Benefit Offered" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvmgtAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mgtamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFmgtAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Revenue">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvrevenue" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFrevenue" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cost">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcost" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocost")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFcost" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="GP % on Cost">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgpper" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gpper")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NP  % on Cost">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvnpper" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Target="_blank" Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "npper")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                                Width="50px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Details">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvinfo" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "link"))%>'
                                                Width="50px">
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Status">
                                        <ItemTemplate>

                                            <asp:Label ID="lgvprost" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="9px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "prjdone")) %>'
                                                Width="40px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>



                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-xs btn-danger"><span class="glyphicon glyphicon-ok"></span>
                                            </asp:HyperLink>


                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pre-Constration Planning">
                                        <ItemTemplate>



                                            <asp:HyperLink ID="lnkbtnPreEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-xs btn-danger"><span class="glyphicon glyphicon-ok"></span>
                                            </asp:HyperLink>


                                        </ItemTemplate>
                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
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




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
