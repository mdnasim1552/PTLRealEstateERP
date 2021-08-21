<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkMgtInterface.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_97_MIS.LinkMgtInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
    
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewServices" runat="server">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Employee Name</asp:Label>
                                                <asp:TextBox ID="txtEmpSrc" runat="server" CssClass="form-control " Width="180px"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">From</asp:Label>

                                                <asp:Label ID="lblDate" runat="server" CssClass=" inputDateBox "></asp:Label>
                                            </div>





                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="row">
                                <asp:GridView ID="gvempservices" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdescription" runat="server" Font-Size="11PX" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "descrip")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDate" runat="server" Font-Size="11PX" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "date")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvComp" runat="server" Font-Size="11PX" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSection" runat="server" Font-Size="11PX" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Increment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIncSalary" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incrsal")).ToString("#, ##0;(#, ##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSalary" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosalary")).ToString("#, ##0;(#, ##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpredesig" runat="server" Font-Size="11PX" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>


                        </asp:View>
                        <asp:View ID="ViewLateInf" runat="server">

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lblempname" runat="server" CssClass="lblTxt lblName">Employee Name</asp:Label>
                                                <asp:TextBox ID="txtSrcEmpName" runat="server" CssClass="inputDateBox"></asp:TextBox>
                                                <asp:LinkButton ID="imgbtnEmpName" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" TabIndex="1" OnClick="imgbtnEmpName_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                            </div>
                                            <div class="col-md-3 pading5px">
                                                <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="form-control inputTxt" AutoPostBack="true">
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-5 pading5px">
                                                <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                                <asp:Label ID="lblFDate" runat="server" CssClass="lblTxt lblName"></asp:Label>

                                                <asp:Label ID="lbltodate" runat="server" CssClass="lblTxt lblName" Text="To:"></asp:Label>
                                                <asp:Label ID="lblTDate" runat="server" CssClass="lblTxt lblName"></asp:Label>
                                            </div>





                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                        </asp:View>
                        <asp:View ID="ViewEmpSalary" runat="server">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Employee Name</asp:Label>
                                                <asp:Label ID="lblEmpNameSal" runat="server" CssClass="form-control " Width="180px"></asp:Label>
                                            </div>





                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:GridView ID="gvSalAdd" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    Width="400px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCodesaladd" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTSalAdd" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-decaration: none;">Total</asp:LinkButton>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvgperadd" runat="server" BackColor="Transparent" Height="20px"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="35px" BorderStyle="None"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgtype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gtype")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvSaladd" runat="server" BackColor="Transparent" Height="20px"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gval")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFSalAdd" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
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
                                <div class="col-md-6">
                                    <asp:GridView ID="gvSalSub" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    Width="400px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCodesalsub" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTSalSub" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-decaration: none;">Total</asp:LinkButton>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgtype0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gtype")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvgpersub" runat="server" BackColor="Transparent" Height="20px"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="35px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvSalSub" runat="server" BackColor="Transparent" Height="20px"
                                                    BorderStyle="None" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gval")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFSalSub" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
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
                            <div class="row">
                                
                            </div>
                           <div class="row">
                               <div class="col-md-12">

                                   
                                <asp:Label ID="lbltxtTotalSal" runat="server"
                                            Text="Net Salary:" Width="126px"></asp:Label>
                                        <asp:Label ID="lbltotalsal" runat="server" ></asp:Label>
                               </div>
                           </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
