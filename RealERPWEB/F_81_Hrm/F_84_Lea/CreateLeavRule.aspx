<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CreateLeavRule.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.CreateLeavRule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

            //$('.chzn-select').chosen({search_contains: true });
            //$(function () {
            //    $('[id*=]').multiselect({
            //        includeSelectAllOption: true
            //    });

            //});
        }

        $(".js-select2").select2({
            closeOnSelect: false,
            placeholder: "Placeholder",
            // allowHtml: true,
            allowClear: true,
            tags: true // создает новые опции на лету
        });


        function isNumberKey(txt, evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                //Check if the text already contains the . character
                if (txt.value.indexOf('.') === -1) {
                    return true;
                } else {
                    return false;
                }
            } else {
                if (charCode > 31 &&
                    (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
        }
    </script>
    <style>
        .chzn-container-single .chzn-single {
            height: 36px !important;
            line-height: 36px;
        }

        body {
            font-family: 'Century Gothic' !important;
        }

        .chzn-container-multi .chzn-choices {
            line-height: 35px;
        }
    </style>



    <div class="card card-fluid container-data mt-5" id='printarea'>
        <div class="card-body">
            <div class="row">
                <div class="col-md-2">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" type="button">Year</button>
                        </div>
                        <asp:DropDownList ID="ddlyear" ClientIDMode="Static" data-placeholder="Choose year" runat="server" CssClass="form-control">
                            <asp:ListItem Value="2020">2020</asp:ListItem>
                            <asp:ListItem Value="2021" Selected="True">2021</asp:ListItem>
                            <asp:ListItem Value="2022">2022</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
            <br />
            <hr />
            <div class="row">
                <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                    ShowFooter="True">
                    <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                        Visible="False" />

                    <Columns>
                        <asp:TemplateField HeaderText="Sl.No.">
                            <ItemTemplate>
                                <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            <ItemStyle Font-Size="12px" />
                        </asp:TemplateField>
                        <%-- <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True">
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>--%>
                        <asp:TemplateField HeaderText=" ">
                            <ItemTemplate>
                                <asp:Label ID="lblgrcode" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod2"))+"-" %>'
                                    Width="20px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Size="12px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                    MaxLength="3"
                                    Style="border-style: none; border-color: midnightblue; font-size: 12px; text-align: left;"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod3")) %>'
                                    Width="50px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod3")) %>'
                                    Width="50px" Style="text-align: left"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            <ItemStyle Font-Size="12px" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lbgrcod1" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")) %>'
                                    Visible="False"></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                    Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                    Width="200px"></asp:TextBox>
                            </EditItemTemplate>

                            <ItemTemplate>
                                <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                    Style="font-size: 12px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                    Width="200px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Leave">


                            <ItemTemplate>
                                <asp:TextBox ID="TxtLeav" runat="server" Font-Size="12px" AutoCompleteType="None" onkeypress="return isNumberKey(this,event);"
                                    Style="font-size: 12px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdescbn")) %>'
                                    Width="200px"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                        </asp:TemplateField>

                    </Columns>
                    <PagerStyle CssClass="gvPagination" />
                    <HeaderStyle CssClass="grvHeader" />
                    <FooterStyle CssClass="grvFooter" />
                    <AlternatingRowStyle BackColor="" />
                </asp:GridView>
            </div>

        </div>
    </div>


</asp:Content>
