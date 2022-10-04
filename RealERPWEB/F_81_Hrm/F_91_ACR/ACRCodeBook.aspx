<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ACRCodeBook.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_91_ACR.ACRCodeBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Scripts/gridviewScrollHaVertworow.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            $(".select2").select2();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.select2').each(function () {
                var select = $(this);
                select.select2({
                    placeholder: 'Select an option',
                    width: '100%',
                    allowClear: !select.prop('required'),
                    language: {
                        noResults: function () {
                            return "{{ __('No results found') }}";
                        }
                    }
                });
            });

        };
    </script>



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
            <div class="card card-fluid">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="LblBookName1" runat="server" CssClass="control-label" Text="Select Code Book:"></asp:Label>
                            <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="form-control">
                            </asp:DropDownList>

                            <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="Details:"></asp:Label>

                            <asp:DropDownList ID="ddlOthersBookSegment" CssClass="form-control" runat="server">
                                <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
                                <asp:ListItem Selected="True" Value="5">Details Code</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-control select2"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" style="margin-top:24px"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="row" style="margin:10px 0px 10pc 0px;">
                        <div class="table-responsive">
                                <asp:GridView ID="gvAcrCBook" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False"
                            OnRowCancelingEdit="gvAcrCBook_RowCancelingEdit" OnRowEditing="gvAcrCBook_RowEditing"
                            OnRowUpdating="gvAcrCBook_RowUpdating" Width="726px" ShowFooter="True" CssClass="table table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                                Visible="False" />
                            <FooterStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True">
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acrgcod2"))+"-" %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                            MaxLength="3"
                                            Style="border-style: none; border-color: midnightblue; font-size: 12px; text-align: left;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acrgcod3")) %>'
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acrgcod3")) %>'
                                            Width="50px" Style="text-align: left"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvgcod" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="250px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True"
                                            Font-Bold="True" Font-Size="14px"
                                            OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                            Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                            Width="150px">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                            Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Details of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvddesc" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ddesc")) %>'
                                            Width="250px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblddesc" runat="server" Font-Size="12px"
                                            Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ddesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <%--
                                   

                                <asp:TemplateField HeaderText="Unit">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvUnit" runat="server" BackColor="White" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Rate">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvRate" runat="server" BackColor="White" BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRate" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Type">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvttpe" runat="server" BackColor="White" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acrgval")) %>'
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtype" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acrgval")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="%">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvtxtpercnt" runat="server" BackColor="White" BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px" Style="text-align: right"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="lgvpercnt" runat="server" BackColor="White" BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                 --%>

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
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

