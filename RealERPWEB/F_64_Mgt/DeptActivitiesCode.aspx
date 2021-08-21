<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="DeptActivitiesCode.aspx.cs" Inherits="RealERPWEB.F_64_Mgt.DeptActivitiesCode" %>

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



                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="LblBookName1" runat="server" CssClass="lblTxt lblName" Text="Select Code"></asp:Label>
                                    </div>



                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-2 pading5px">
                                        <asp:DropDownList ID="ddlOthersBookSegment" CssClass="form-control inputTxt" runat="server">
                                            
                                             <asp:ListItem Value="2">Main Code</asp:ListItem>
                                            <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                            <asp:ListItem Value="7">Sub Code-2</asp:ListItem>
                                            <asp:ListItem Value="9">Sub Code-3</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12">Details Code</asp:ListItem>

                                            

                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-3 pading5px">

                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkok_Click">Ok</asp:LinkButton>

                                        </div>

                                        <div class="msgHandSt pull-right">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>



                                    </div>

                                </div>



                            </div>
                        </fieldset>

                    </div>
                    <div class="row">
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False"
                            OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating" Width="572px" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" PageSize="15" OnPageIndexChanging="grvacc_PageIndexChanging">

                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                Mode="NumericFirstLast" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True"></asp:CommandField>
                                <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod2")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server"
                                            MaxLength="13"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod4")) %>'
                                            Width="80px" BorderStyle="none"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod4")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>


                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" MaxLength="100"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc")) %>'
                                            Width="250px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Duration" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvduration" runat="server" MaxLength="100"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "stddur")) %>'
                                            Width="80px" Style="text-align:right;"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvduration" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "stddur")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod")) %>'
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvttpe" runat="server" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgval")) %>'
                                            Width="30px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtype" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgval")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Department">
                                        <EditItemTemplate>
                                            <asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                                BorderWidth="1px">


                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtSearchDeptName" runat="server" CssClass=" inputtextbox" TabIndex="4" Width="86px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="ibtnSrchdept" runat="server" OnClick="ibtnSrchdept_Click" CssClass="btn btn-success srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlDeptName" runat="server" CssClass="ddlPage" Width="200px" TabIndex="6">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdeptname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Marks">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvmarks" runat="server" MaxLength="100"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px" Style="text-align:right"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvmarks" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px" Style="text-align:right">></asp:Label>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                               

                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>


                    </div>

                    <!-- end of Content Part-->

                </div>
                <!-- end of container Part-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



