
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptlinkAccAITVATASD.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptlinkAccAITVATASD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



      <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            //$("input, select").bind("keydown", function (event) {
            //    var k1 = new KeyPress();
            //    k1.textBoxHandler(event);
           // });

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">


                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">


                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Form:"></asp:Label>
                                        <asp:Label ID="lblvalfrmdate" runat="server" CssClass="inputtextbox"></asp:Label>

                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to" Text="To:"></asp:Label>
                                        <asp:Label ID="lblvaltodate" runat="server" CssClass="inputtextbox"></asp:Label>

                                          <div class="colMdbtn pading5px">
                                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn"  OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                            </div>



                                    </div>

                                    <div class="col-md-4 pading5px ">

                                        
                                         <label id="lbldaywise"  runat="server" class="switch">
                                            <asp:CheckBox ID="Checkdaywise" runat="server"  /> <span class="btn btn-xs slider round"></span>   </label>
                                            <asp:Label ID="lbltxtdaywise" runat="server" Text="Day Wise" CssClass="btn btn-xs"></asp:Label>
                                        </div>
                                   

                                    

                                </div>


                                <div class="form-group">


                                    <div class="col-md-12 pading5px ">
                                        <asp:Label ID="lblresDescription" runat="server" CssClass="lblTxt lblName" Text="Resouce Desc.:"></asp:Label>
                                        <asp:Label ID="lblValresDescription" runat="server" CssClass="inputtextbox" Style="width: 250px;"></asp:Label>

                                        <asp:Label ID="lblSubDescription" runat="server" CssClass=" smLbl_to" Text="Supplier/Contractor:"></asp:Label>
                                        <asp:Label ID="lblValSubDescription" runat="server" CssClass="inputtextbox" Style="width: 250px;"></asp:Label>




                                    </div>
                                </div>


                            </div>
                        </fieldset>

                        <asp:GridView ID="gvaitvsd" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="650px" CssClass="table table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvaitvsd_RowDataBound">
                            <PagerSettings Position="Top" />
                            <RowStyle />

                            <Columns>
                                <asp:TemplateField HeaderText="Project Name">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                            Text="Total :"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProjectName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vou.Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvoudate" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvounum" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="85px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOpAmount" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFOpAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="100px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDrAmount" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFDrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="100px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCrAmount" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="100px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closing">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvClAmount" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFClsAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="100px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChqNo" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle VerticalAlign="Top" />
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
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

